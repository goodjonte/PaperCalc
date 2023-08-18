using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.AspeosStockCoatings
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

      public PaperCalc.DTOs.AspeosStockCoatingsDTO AspeosStockCoatingsDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.AspeosStockCoatings == null)
            {
                return NotFound();
            }

            var aspeosstockcoatings = await _context.AspeosStockCoatings.FirstOrDefaultAsync(m => m.Id == id);
            if (aspeosstockcoatings == null)
            {
                return NotFound();
            }
            else
            {
                AspeosStockCoatingsDTO = new()
                {
                    Name = aspeosstockcoatings.Name,
                    CoatingId = aspeosstockcoatings.Id,
                    AveragePrice = aspeosstockcoatings.GetAverage(_context),
                    NumberOfPapers = aspeosstockcoatings.GetNumberOfPapers(_context)
                };
            }
            return Page();
        }
    }
}
