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
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.LaminationStock == null)
            {
                return NotFound();
            }
            var laminationstock = await _context.LaminationStock.FindAsync(id);

            if (laminationstock != null)
            {
                LaminationStock = laminationstock;
                _context.LaminationStock.Remove(LaminationStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
