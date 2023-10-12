using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PaperCalc.Pages.FlatStock
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IConfiguration _configuration;


        public IndexModel(PaperCalc.Data.PaperCalcContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IList<PaperCalc.Models.FlatStock> FlatStock { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            if (_context.FlatStock != null)
            {
                FlatStock = await _context.FlatStock.ToListAsync();
            }
            return Page();
        }


    }
}
