using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.DTOs;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.WideFormat
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            Paper = _context.WideFormatStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.WideFormat).ToList();
            _env = env;
        }
        public WideFormatFormInputs WideFormatFormInputs { get; set; } = default!;
        public List<WideFormatStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == WideFormatFormInputs.Id).First(); } }
        [BindProperty]
        public WideFormatCalculation Calculation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.WideFormatFormInputs == null)
            {
                return NotFound();
            }

            var wideFormatFormInputs =  await _context.WideFormatFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (wideFormatFormInputs == null)
            {
                return NotFound();
            }
            WideFormatFormInputs = wideFormatFormInputs;

            Calculation = new(_context, _env.ContentRootPath, WideFormatFormInputs);
            return Page();
        }
    }
}
