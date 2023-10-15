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

namespace PaperCalc.Pages.Stock.Binding.Cover
{
    public class EditModel : PageModel
    {
        private readonly PaperCalcContext _context;

        public EditModel(PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BindingCoverStock BindingCoverStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BindingCoverStock == null)
            {
                return NotFound();
            }

            var bindingcoverstock = await _context.BindingCoverStock.FirstOrDefaultAsync(m => m.Id == id);
            if (bindingcoverstock == null)
            {
                return NotFound();
            }
            BindingCoverStock = bindingcoverstock;
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

            _context.Attach(BindingCoverStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BindingCoverStockExists(BindingCoverStock.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool BindingCoverStockExists(Guid id)
        {
            return (_context.BindingCoverStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
