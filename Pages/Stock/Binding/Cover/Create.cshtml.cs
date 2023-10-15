using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.Binding.Cover
{
    public class CreateModel : PageModel
    {
        private readonly PaperCalcContext _context;

        public CreateModel(PaperCalcContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BindingCoverStock BindingCoverStock { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.BindingCoverStock == null || BindingCoverStock == null)
            {
                return Page();
            }

            _context.BindingCoverStock.Add(BindingCoverStock);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
