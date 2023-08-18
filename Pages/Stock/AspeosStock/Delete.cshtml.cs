using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.AspeosStock
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.AspeosStock AspeosStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.AspeosStock == null)
            {
                return NotFound();
            }

            var aspeosstock = await _context.AspeosStock.FirstOrDefaultAsync(m => m.Id == id);

            if (aspeosstock == null)
            {
                return NotFound();
            }
            else 
            {
                AspeosStock = aspeosstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.AspeosStock == null)
            {
                return NotFound();
            }
            var aspeosstock = await _context.AspeosStock.FindAsync(id);

            if (aspeosstock != null)
            {
                AspeosStock = aspeosstock;
                _context.AspeosStock.Remove(AspeosStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
