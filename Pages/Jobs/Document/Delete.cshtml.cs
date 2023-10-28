using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.Document
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DocumentFormInputs DocumentFormInputs { get; set; } = default!;
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == DocumentFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.DocumentFormInputs == null)
            {
                return NotFound();
            }

            var documentFormInputs = await _context.DocumentFormInputs.FirstOrDefaultAsync(m => m.Id == id);

            if (documentFormInputs == null)
            {
                return NotFound();
            }
            else
            {
                DocumentFormInputs = documentFormInputs;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.DocumentFormInputs == null)
            {
                return NotFound();
            }
            var documentFormInputs = await _context.DocumentFormInputs.FindAsync(id);

            if (documentFormInputs != null)
            {
                DocumentFormInputs = documentFormInputs;
                _context.DocumentFormInputs.Remove(DocumentFormInputs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Details", new { id = Connection.JobId.ToString() });
        }
    }
}
