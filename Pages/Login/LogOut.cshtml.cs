using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PaperCalc.Pages.Login
{
    public class LogOutModel : PageModel
    {
        public ActionResult OnGet()
        {
            Response.Cookies.Delete("Parrot");

            return RedirectToPage("./Index");
        }
    }
}
