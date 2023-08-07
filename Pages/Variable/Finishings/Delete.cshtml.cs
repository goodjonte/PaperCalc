using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Finishings
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.Finishings Finishings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Finishings == null)
            {
                return NotFound();
            }

            var finishings = await _context.Finishings.FirstOrDefaultAsync(m => m.Id == id);

            if (finishings == null)
            {
                return NotFound();
            }
            else 
            {
                Finishings = finishings;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Finishings == null)
            {
                return NotFound();
            }
            var finishings = await _context.Finishings.FindAsync(id);

            if (finishings != null)
            {
                Finishings = finishings;
                _context.Finishings.Remove(Finishings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
