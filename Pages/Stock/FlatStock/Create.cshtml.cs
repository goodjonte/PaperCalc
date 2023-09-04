using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.FlatStock
{
    public class CreateModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;

        public CreateModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            return Page();
        }

        [BindProperty]
        public PaperCalc.Models.FlatStock FlatStock { get; set; } = default!;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FlatStock == null || FlatStock == null)
            {
                return Page();
            }

            _context.FlatStock.Add(FlatStock);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
