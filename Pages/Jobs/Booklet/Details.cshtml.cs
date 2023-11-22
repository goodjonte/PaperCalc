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

namespace PaperCalc.Pages.Jobs.Booklet
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
            _env = env;
        }
        public BookletFormInputs BookletFormInputs { get; set; } = default!;
        public List<Sra3AndBookletsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == BookletFormInputs.Id).First(); } }
        [BindProperty]
        public BookletCalculation Calculation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BookletFormInputs == null)
            {
                return NotFound();
            }

            var bookFormInput =  await _context.BookletFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (bookFormInput == null)
            {
                return NotFound();
            }
            BookletFormInputs = bookFormInput;

            Calculation = new(_context, _env.ContentRootPath, BookletFormInputs);
            return Page();
        }
    }
}
