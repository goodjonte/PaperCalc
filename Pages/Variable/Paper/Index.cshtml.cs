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
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.Paper> Paper { get;set; } = default!;
        public IList<PaperCalc.Models.Size> Size { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Paper != null)
            {
                Size = await _context.Size.ToListAsync();
                Paper = await _context.Paper.ToListAsync();
            }
        }
    }
}
