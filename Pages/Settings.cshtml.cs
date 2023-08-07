using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Models;

namespace PaperCalc.Pages
{
    public class SettingsModel : PageModel
    {
        [BindProperty]
        public PaperCalc.Models.Settings Settings { get; set; } = default!;
        public PaperCalc.Data.PaperCalcContext _context { get; set; } = default!;
        public SettingsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            if (_context.Settings != null)
            {
                Settings = _context.Settings.FirstOrDefault();
            }
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Settings not saved - err";
            }
            _context.Attach(Settings).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            ViewData["Message"] = "Settings saved successfully";
            return Page();
        }
    }
}
