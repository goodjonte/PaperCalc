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
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

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

            var sra3stock = await _context.Sra3AndBookletsStock.FirstOrDefaultAsync(m => m.Id == id);
            if (sra3stock == null)
            {
                return NotFound();
            }
            else 
            {
                Sra3Stock = sra3stock;
            }
            return Page();
        }
    }
}
