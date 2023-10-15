using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.BindingCovers.NewFolder
{
    public class CreateModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public CreateModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BindingCoilsStock BindingCoilsStock { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BindingCoilsStock == null || BindingCoilsStock == null)
            {
                return Page();
            }

            _context.BindingCoilsStock.Add(BindingCoilsStock);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
