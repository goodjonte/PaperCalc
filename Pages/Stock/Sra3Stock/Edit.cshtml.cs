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

namespace PaperCalc.Pages.Sra3Stock
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;

        public EditModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public PaperCalc.Models.Sra3AndBookletsStock Sra3Stock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.Sra3AndBookletsStock == null)
            {
                return NotFound();
            }

            var sra3stock =  await _context.Sra3AndBookletsStock.FirstOrDefaultAsync(m => m.Id == id);
            if (sra3stock == null)
            {
                return NotFound();
            }
            Sra3Stock = sra3stock;
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

            _context.Attach(Sra3Stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!Sra3StockExists(Sra3Stock.Id))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool Sra3StockExists(Guid id)
        {
          return (_context.Sra3AndBookletsStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
