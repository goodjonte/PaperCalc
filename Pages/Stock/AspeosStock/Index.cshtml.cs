using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PaperCalc.Pages.AspeosStock
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.AspeosStock> AspeosStock { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AspeosStock != null)
            {
                AspeosStock = await _context.AspeosStock.ToListAsync();
            }
        }


    }
}
