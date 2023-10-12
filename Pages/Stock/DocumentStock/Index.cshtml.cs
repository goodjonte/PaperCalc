using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PaperCalc.Pages.DocumentsStock
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
        [BindProperty]
        public IList<PaperCalc.Models.DocumentsStock> DocumentsStock { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }
            if (_context.DocumentsStock != null)
            {
                DocumentsStock = await _context.DocumentsStock.ToListAsync();
            }
            return Page();
        }


    }
}
