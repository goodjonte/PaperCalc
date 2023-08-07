using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Paper
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

      public PaperCalc.Models.Paper Paper { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Paper == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper.FirstOrDefaultAsync(m => m.Id == id);
            if (paper == null)
            {
                return NotFound();
            }
            else 
            {
                Paper = paper;
            }
            return Page();
        }
    }
}
