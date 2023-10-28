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
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            Paper = _context.DocumentsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Document).ToList();
        }

        [BindProperty]
        public DocumentFormInputs DocumentFormInputs { get; set; } = default!;
        public List<Models.DocumentsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == DocumentFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.DocumentFormInputs == null)
            {
                return NotFound();
            }

            var documentFormInputs =  await _context.DocumentFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (documentFormInputs == null)
            {
                return NotFound();
            }
            DocumentFormInputs = documentFormInputs;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DocumentFormInputs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentFormInputsExists(DocumentFormInputs.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Details", new { id = Connection.JobId.ToString() });
        }

        private bool DocumentFormInputsExists(Guid id)
        {
          return (_context.DocumentFormInputs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
