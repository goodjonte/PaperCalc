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
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

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
    }
}
