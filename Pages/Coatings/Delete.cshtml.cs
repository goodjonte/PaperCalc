using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Coatings
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.Coatings Coatings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Coatings == null)
            {
                return NotFound();
            }

            var coatings = await _context.Coatings.FirstOrDefaultAsync(m => m.Id == id);

            if (coatings == null)
            {
                return NotFound();
            }
            else 
            {
                Coatings = coatings;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Coatings == null)
            {
                return NotFound();
            }
            var coatings = await _context.Coatings.FindAsync(id);

            if (coatings != null)
            {
                Coatings = coatings;
                _context.Coatings.Remove(Coatings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
