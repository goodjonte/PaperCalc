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
    public class DetailsModel : PageModel
    {
        private readonly PaperCalcContext _context;

        public DetailsModel(PaperCalcContext context)
        {
            _context = context;
        }

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
    }
}
