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

namespace PaperCalc.Pages.Jobs.Sra3
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Sra3).ToList();
            _env = env;
        }
        public Sra3FormInput Sra3FormInput { get; set; } = default!;
        public List<Sra3AndBookletsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == Sra3FormInput.Id).First(); } }
        [BindProperty]
        public Sra3Calculation Calculation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.WideFormatFormInputs == null)
            {
                return NotFound();
            }

            var sra3FormInput =  await _context.Sra3FormInput.FirstOrDefaultAsync(m => m.Id == id);
            if (sra3FormInput == null)
            {
                return NotFound();
            }
            Sra3FormInput = sra3FormInput;

            Calculation = new(_context, _env.ContentRootPath, Sra3FormInput);
            return Page();
        }
    }
}
