using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public IList<PaperCalc.Models.Paper> Paper { get; set; } = default!;
        public IList<PaperCalc.Models.Size> Size { get; set; } = default!;
        public IList<PaperCalc.Models.Colour> Colour { get; set; } = default!;
        public IList<PaperCalc.Models.Clicks> Clicks { get; set; } = default!;
        public IList<PaperCalc.Models.Finishings> Finishings { get; set; } = default!;
        public Quote Quote { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Paper != null)
            {
                Paper = await _context.Paper.ToListAsync();
                Colour = await _context.Colour.ToListAsync();
                Size = await _context.Size.ToListAsync();
                Clicks = await _context.Clicks.ToListAsync();
                Finishings = await _context.Finishings.ToListAsync();
            }
        }

        public async Task OnPostAsync()
        {
            if (_context.Paper != null)
            {
                Paper = await _context.Paper.ToListAsync();
                Colour = await _context.Colour.ToListAsync();
                Size = await _context.Size.ToListAsync();
                Clicks = await _context.Clicks.ToListAsync();
                Finishings = await _context.Finishings.ToListAsync();
            }


            await Quote.SetObjects(_context);
            ViewData["total"] = Quote.GetTotal();
            //ViewData["TotalWarning"]
        }
    }

}