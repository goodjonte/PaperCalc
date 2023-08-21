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
        private IWebHostEnvironment _env;

        public IndexModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            Settings.SetSettings(_env.ContentRootPath);
            Paper = _context.AspeosStock.ToList();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public IList<PaperCalc.Models.AspeosStock> Paper { get; set; } = default!;

        public IList<PaperCalc.Models.AspeosFlatSize> FlatSize { get; set; } = default!;
        public AspeosCalculation? AspeosCalculation { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (_context.AspeosStock != null)
            {
                AspeosCalculation = new AspeosCalculation();
                FlatSize = await _context.AspeosFlatSizes.ToListAsync();
            }
      
            return Page();
        }

        public void OnPost()
        {
            if(AspeosCalculation != null)
            {
                AspeosCalculation.Calculate(_context, _env.ContentRootPath);
                FlatSize = _context.AspeosFlatSizes.ToList();
                Paper = _context.AspeosStock.ToList();
            }
            if(AspeosCalculation != null && AspeosCalculation.FileHandlingFee != null)
            {
                Settings = new();
                Settings.FilehandlingCost = (double)AspeosCalculation.FileHandlingFee;
            }
        }
    }
}