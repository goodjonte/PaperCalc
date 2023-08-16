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
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            AspeosStockCoatings = new List<PaperCalc.DTOs.AspeosStockCoatingsDTO>();
        }

        public List<PaperCalc.DTOs.AspeosStockCoatingsDTO> AspeosStockCoatings { get;set; }

        public async Task OnGetAsync()
        {
            if (_context.AspeosStockCoatings != null)
            {
                IList <PaperCalc.Models.AspeosStockCoatings> ASCs = await _context.AspeosStockCoatings.ToListAsync();
                foreach(PaperCalc.Models.AspeosStockCoatings asc in ASCs)
                {
                    PaperCalc.DTOs.AspeosStockCoatingsDTO aspDTO = new()
                    {
                        CoatingId = asc.Id,
                        Name = asc.Name,
                        AveragePrice = asc.GetAverage(_context),
                        NumberOfPapers = asc.GetNumberOfPapers(_context)
                    };
                    AspeosStockCoatings.Add(aspDTO);
                }
            }
        }
    }
}
