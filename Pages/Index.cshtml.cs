using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PaperCalc.DTOs;
//using PaperCalc.Data;
using PaperCalc.Models;
using System.Drawing;

namespace PaperCalc.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.AspeosStock> Paper { get; set; } = default!;
        public IList<PaperCalc.Models.AspeosStockCoatings> Coatings { get; set; } = default!;

        public IList<PaperCalc.Models.AspeosFlatSize> FlatSize { get; set; } = default!;
        public AspeosCalculation? AspeosCalculation { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.AspeosStock != null)
            {
                //Paper = await _context.Paper.ToListAsync();
                Coatings = await _context.AspeosStockCoatings.ToListAsync();
                AspeosCalculation = new AspeosCalculation();
                FlatSize = await _context.AspeosFlatSizes.ToListAsync();
            }
        }

        public void OnPost()
        {
            if(AspeosCalculation != null)
            {
                AspeosCalculation.Calculate(_context);
                Coatings = _context.AspeosStockCoatings.ToList();
                FlatSize = _context.AspeosFlatSizes.ToList();
            }
        }
    }
}