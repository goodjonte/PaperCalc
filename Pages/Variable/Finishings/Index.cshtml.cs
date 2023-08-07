using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Finishings
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.Finishings> Finishings { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Finishings != null)
            {
                Finishings = await _context.Finishings.ToListAsync();
            }
        }
    }
}
