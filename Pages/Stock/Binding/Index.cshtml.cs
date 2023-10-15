using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.BindingCovers
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<BindingCoverStock> BindingCoverStock { get;set; } = default!;
        public IList<BindingCoilsStock> BindingCoilsStock { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BindingCoverStock != null)
            {
                BindingCoverStock = await _context.BindingCoverStock.ToListAsync();
            }
            if (_context.BindingCoilsStock != null)
            {
                BindingCoilsStock = await _context.BindingCoilsStock.ToListAsync();
            }
        }
    }
}
