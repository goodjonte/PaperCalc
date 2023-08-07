using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Clicks
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.Clicks Clicks { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Clicks == null)
            {
                return NotFound();
            }

            var clicks = await _context.Clicks.FirstOrDefaultAsync(m => m.Id == id);

            if (clicks == null)
            {
                return NotFound();
            }
            else 
            {
                Clicks = clicks;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Clicks == null)
            {
                return NotFound();
            }
            var clicks = await _context.Clicks.FindAsync(id);

            if (clicks != null)
            {
                Clicks = clicks;
                _context.Clicks.Remove(Clicks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
