using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.WideFormatStock
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.WideFormatStock WideFormatStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.WideFormatStock == null)
            {
                return NotFound();
            }

            var wideformatstock = await _context.WideFormatStock.FirstOrDefaultAsync(m => m.Id == id);

            if (wideformatstock == null)
            {
                return NotFound();
            }
            else 
            {
                WideFormatStock = wideformatstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.WideFormatStock == null)
            {
                return NotFound();
            }
            var wideformatstock = await _context.WideFormatStock.FindAsync(id);

            if (wideformatstock != null)
            {
                WideFormatStock = wideformatstock;
                _context.WideFormatStock.Remove(WideFormatStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
