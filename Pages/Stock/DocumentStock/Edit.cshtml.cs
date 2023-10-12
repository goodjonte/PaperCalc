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

namespace PaperCalc.Pages.DocumentsStock
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
        public PaperCalc.Models.DocumentsStock DocumentsStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.DocumentsStock == null)
            {
                return NotFound();
            }

            var documentsStock =  await _context.DocumentsStock.FirstOrDefaultAsync(m => m.Id == id);
            if (documentsStock == null)
            {
                return NotFound();
            }
            DocumentsStock = documentsStock;
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

            _context.Attach(DocumentsStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!DocumentsStockExists(DocumentsStock.Id))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool DocumentsStockExists(Guid id)
        {
          return (_context.DocumentsStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
