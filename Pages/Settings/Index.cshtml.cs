using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Models;
using System.Diagnostics;

namespace PaperCalc.Pages.Settings
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            AspeosFlatSizes = new();
            Settings = new();
            AspeosFlatSize = new()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };
        }

        [BindProperty]
        public PaperCalc.DTOs.Settings Settings { get; set; }
        [BindProperty]
        public List<PaperCalc.Models.AspeosFlatSize> AspeosFlatSizes { get; set; }
        [BindProperty]
        public PaperCalc.Models.AspeosFlatSize AspeosFlatSize { get; set; }
        public void OnGet()
        {
            AspeosFlatSizes = _context.AspeosFlatSizes.ToList();
            Debug.WriteLine(AspeosFlatSizes.Count);
        }

        public ActionResult OnPostSaveSettings()
        {
            Settings.SaveSettings();
            return RedirectToPage("./Index");
        }
        public ActionResult OnPostAspeosCreate()
        {
            if(!ModelState.IsValid || AspeosFlatSize == null)
            {
                return Page();
            }

            _context.AspeosFlatSizes.Add(AspeosFlatSize);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        public ActionResult OnPostAspeosEdit()
        {
            if(AspeosFlatSize == null)
            {
                return Page();
            }
            if(string.Equals(AspeosFlatSize.Name, "delete", StringComparison.OrdinalIgnoreCase))
            {
                _context.AspeosFlatSizes.Remove(AspeosFlatSize);
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }
            
            _context.Attach(AspeosFlatSize).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
