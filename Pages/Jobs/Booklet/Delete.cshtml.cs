using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.Booklet
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookletFormInputs BookletFormInputs { get; set; } = default!;
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == BookletFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BookletFormInputs == null)
            {
                return NotFound();
            }

            var bookletFormInputs = await _context.BookletFormInputs.FirstOrDefaultAsync(m => m.Id == id);

            if (bookletFormInputs == null)
            {
                return NotFound();
            }
            else
            {
                BookletFormInputs = bookletFormInputs;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.BookletFormInputs == null)
            {
                return NotFound();
            }
            var bookletFormInputs = await _context.BookletFormInputs.FindAsync(id);

            if (bookletFormInputs != null)
            {
                BookletFormInputs = bookletFormInputs;
                _context.BookletFormInputs.Remove(BookletFormInputs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Details", new { id = Connection.JobId.ToString() });
        }
    }
}
