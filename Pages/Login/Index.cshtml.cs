using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging;
using PaperCalc.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace PaperCalc.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        public IndexModel(PaperCalcContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Password { get; set; } = default!;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //CREATE LOGIN 
            //using var hmac = new HMACSHA512();
            //byte[] passwordSalt = hmac.Key;
            //byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            //_context.Login.Add(new PaperCalc.Models.Login { Id = Guid.NewGuid(), Hash = System.Convert.ToBase64String(passwordHash), Salt = System.Convert.ToBase64String(passwordSalt) });
            //_context.SaveChanges();
            if(PaperCalc.Models.Login.ValidatePassword(_context, Password, false))
            {
                Response.Cookies.Append("PaperCalc", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Password)));
                Response.Redirect("../Index");
            }
            
        }
    }
}
