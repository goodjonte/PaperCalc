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
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.Clicks> Clicks { get;set; } = default!;
        public IList<PaperCalc.Models.Colour> Colours { get; set; } = default!;
        public IList<PaperCalc.Models.Size> Sizes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Clicks != null)
            {
                Clicks = await _context.Clicks.ToListAsync();
                Colours = await _context.Colour.ToListAsync();
                Sizes = await _context.Size.ToListAsync();
            }
        }
    }
}
