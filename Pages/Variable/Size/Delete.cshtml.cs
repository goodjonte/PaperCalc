using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Size
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.Size Size { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Size == null)
            {
                return NotFound();
            }

            var size = await _context.Size.FirstOrDefaultAsync(m => m.Id == id);

            if (size == null)
            {
                return NotFound();
            }
            else 
            {
                Size = size;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Size == null)
            {
                return NotFound();
            }
            var size = await _context.Size.FindAsync(id);

            if (size != null)
            {
                Size = size;
                _context.Size.Remove(Size);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
