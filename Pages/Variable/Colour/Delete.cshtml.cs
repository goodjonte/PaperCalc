using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Colour
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.Colour Colour { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Colour == null)
            {
                return NotFound();
            }

            var colour = await _context.Colour.FirstOrDefaultAsync(m => m.Id == id);

            if (colour == null)
            {
                return NotFound();
            }
            else 
            {
                Colour = colour;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Colour == null)
            {
                return NotFound();
            }
            var colour = await _context.Colour.FindAsync(id);

            if (colour != null)
            {
                Colour = colour;
                _context.Colour.Remove(Colour);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
