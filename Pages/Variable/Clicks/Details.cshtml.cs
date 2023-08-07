using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Clicks
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

      public PaperCalc.Models.Clicks Clicks { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Clicks == null)
            {
                return NotFound();
            }

            var clicks = await _context.Clicks.FirstOrDefaultAsync(m => m.Id == id);
            if (clicks == null)
            {
                return NotFound();
            }
            else 
            {
                Clicks = clicks;
            }
            return Page();
        }
    }
}
