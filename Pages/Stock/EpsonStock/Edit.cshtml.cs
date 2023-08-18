using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.EpsonStock
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaperCalc.Models.EpsonStock EpsonStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.EpsonStock == null)
            {
                return NotFound();
            }

            var epsonstock =  await _context.EpsonStock.FirstOrDefaultAsync(m => m.Id == id);
            if (epsonstock == null)
            {
                return NotFound();
            }
            EpsonStock = epsonstock;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EpsonStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EpsonStockExists(EpsonStock.Id))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool EpsonStockExists(Guid id)
        {
          return (_context.EpsonStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
