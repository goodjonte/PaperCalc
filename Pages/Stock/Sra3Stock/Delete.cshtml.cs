using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Sra3Stock
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
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

            var sra3Stock = await _context.Sra3AndBookletsStock.FirstOrDefaultAsync(m => m.Id == id);

            if (sra3Stock == null)
            {
                return NotFound();
            }
            else 
            {
                Sra3Stock = sra3Stock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Sra3AndBookletsStock == null)
            {
                return NotFound();
            }
            var sra3Stock = await _context.Sra3AndBookletsStock.FindAsync(id);

            if (sra3Stock != null)
            {
                _context.Sra3AndBookletsStock.Remove(sra3Stock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
