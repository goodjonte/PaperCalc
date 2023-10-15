using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.BindingCovers.NewFolder
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BindingCoilsStock BindingCoilsStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BindingCoilsStock == null)
            {
                return NotFound();
            }

            var bindingcoilsstock = await _context.BindingCoilsStock.FirstOrDefaultAsync(m => m.Id == id);

            if (bindingcoilsstock == null)
            {
                return NotFound();
            }
            else 
            {
                BindingCoilsStock = bindingcoilsstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.BindingCoilsStock == null)
            {
                return NotFound();
            }
            var bindingcoilsstock = await _context.BindingCoilsStock.FindAsync(id);

            if (bindingcoilsstock != null)
            {
                BindingCoilsStock = bindingcoilsstock;
                _context.BindingCoilsStock.Remove(BindingCoilsStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
