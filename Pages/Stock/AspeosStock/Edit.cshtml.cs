using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.AspeosStock
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaperCalc.Models.AspeosStock AspeosStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.AspeosStock == null)
            {
                return NotFound();
            }

            var aspeosstock =  await _context.AspeosStock.FirstOrDefaultAsync(m => m.Id == id);
            if (aspeosstock == null)
            {
                return NotFound();
            }
            AspeosStock = aspeosstock;
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

            _context.Attach(AspeosStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AspeosStockExists(AspeosStock.Id))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool AspeosStockExists(Guid id)
        {
          return (_context.AspeosStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
