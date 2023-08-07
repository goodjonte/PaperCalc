using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Colour
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

      public PaperCalc.Models.Colour Colour { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Colour == null)
            {
                return NotFound();
            }

            var colour = await _context.Colour.FirstOrDefaultAsync(m => m.Id == id);
            if (colour == null)
            {
                return NotFound();
            }
            else 
            {
                Colour = colour;
            }
            return Page();
        }
    }
}
