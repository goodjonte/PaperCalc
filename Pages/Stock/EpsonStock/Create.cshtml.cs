using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.EpsonStock
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
        public PaperCalc.Models.EpsonStock EpsonStock { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( _context.EpsonStock == null || EpsonStock == null)
            {
                return Page();
            }

            _context.EpsonStock.Add(EpsonStock);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
