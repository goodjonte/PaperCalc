using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Variable.Clicks
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
            ViewData["ColourId"] = new SelectList(_context.Colour, "Id", "Name");
            ViewData["SizeId"] = new SelectList(_context.Size, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PaperCalc.Models.Clicks Clicks { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Clicks == null || Clicks == null)
            {
                return Page();
            }

            _context.Clicks.Add(Clicks);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
