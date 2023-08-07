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
      

        public async Task OnGetAsync()
        {
            if (_context.Paper != null)
            {
                Paper = await _context.Paper.ToListAsync();
            }
        }

        
    }

}