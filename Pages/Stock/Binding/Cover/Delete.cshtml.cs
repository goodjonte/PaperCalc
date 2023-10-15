using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.Binding.Cover
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalcContext _context;

        public DeleteModel(PaperCalcContext context)
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
            else
            {
                BindingCoverStock = bindingcoverstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.BindingCoverStock == null)
            {
                return NotFound();
            }
            var bindingcoverstock = await _context.BindingCoverStock.FindAsync(id);

            if (bindingcoverstock != null)
            {
                BindingCoverStock = bindingcoverstock;
                _context.BindingCoverStock.Remove(BindingCoverStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
