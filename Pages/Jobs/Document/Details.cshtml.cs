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

namespace PaperCalc.Pages.Jobs.Document
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            Paper = _context.DocumentsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Document).ToList();
            _env = env;
        }
        public DocumentFormInputs DocumentFormInputs { get; set; } = default!;
        public List<Models.DocumentsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == DocumentFormInputs.Id).First(); } }
        [BindProperty]
        public DocumentCalculation Calculation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.DocumentFormInputs == null)
            {
                return NotFound();
            }

            var docFormInput =  await _context.DocumentFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (docFormInput == null)
            {
                return NotFound();
            }
            DocumentFormInputs = docFormInput;

            Calculation = new(_context, _env.ContentRootPath, DocumentFormInputs);
            return Page();
        }
    }
}
