using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.FlatStock
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public PaperCalc.Models.FlatStock FlatStock { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.FlatStock == null)
            {
                return NotFound();
            }

            var flatStock = await _context.FlatStock.FirstOrDefaultAsync(m => m.Id == id);
            if (flatStock == null)
            {
                return NotFound();
            }
            else
            {
                FlatStock = flatStock;
            }
            return Page();
        }
    }
}
