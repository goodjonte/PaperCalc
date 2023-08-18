using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.EpsonStock
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.EpsonStock> EpsonStock { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (_context.EpsonStock != null)
            {
                EpsonStock = await _context.EpsonStock.ToListAsync();
            }
            return Page();
        }
    }
}
