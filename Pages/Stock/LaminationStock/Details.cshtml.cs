using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.LaminationStock
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

      public Models.LaminationStock LaminationStock { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.LaminationStock == null)
            {
                return NotFound();
            }

            var laminationstock = await _context.LaminationStock.FirstOrDefaultAsync(m => m.Id == id);
            if (laminationstock == null)
            {
                return NotFound();
            }
            else 
            {
                LaminationStock = laminationstock;
            }
            return Page();
        }
    }
}
