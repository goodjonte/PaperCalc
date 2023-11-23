using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PaperCalc.DTOs;
using PaperCalc.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PaperCalc.Pages.Settings
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;
        private IConfiguration _configuration;
        public IndexModel(IConfiguration config,PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            FlatSizes = new();
            _env = env;
            Settings = new();
            NewUser = new();
           
            _configuration = config;
        }
        [BindProperty]
        public PaperCalc.DTOs.NewUserDTO NewUser { get; set; }
        [BindProperty]
        public PaperCalc.DTOs.Settings Settings { get; set; }
        [BindProperty]
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public FlatSize FlatSize { get; set; }


        //GET Method
        public IActionResult OnGet()
        {
            Settings.SetSettings(_env.ContentRootPath);

            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }

            FlatSizes = _context.FlatSizes.ToList();
            return Page();
        }

        //Post Methods
        public ActionResult OnPostSaveSettings()
        {
            Settings.SaveSettings(_env.ContentRootPath);
            return RedirectToPage("./Index");
        }

        //Post Methods
        public ActionResult OnPostSaveLabourSettings()
        {
            Settings.SaveSettings(_env.ContentRootPath);
            return RedirectToPage("./Index");
        }

        //FlatSize Methods
        public IActionResult OnPostFlatsizeEdit()
        {
            if(FlatSize == null)
            {
                return NotFound();
            }
            if (string.Equals(FlatSize.Name, "delete", StringComparison.OrdinalIgnoreCase))
            {
                var flatsize = _context.FlatSizes.Find(FlatSize.Id);

                if (flatsize != null)
                {
                    _context.FlatSizes.Remove(flatsize);

                }
                _context.SaveChanges();

                return RedirectToPage("./Index");
            }

            _context.Attach(FlatSize).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        public IActionResult OnPostFlatsizeCreate()
        {
            if (FlatSize == null)
            {
                return NotFound();
            }
            FlatSize.Id = Guid.NewGuid();
            _context.FlatSizes.Add(FlatSize);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        //User methods
        public async Task<IActionResult> OnPostCreateUser()
        {
            if(NewUser.UserName.IsNullOrEmpty() || NewUser.Password.IsNullOrEmpty())
            {
                NewUser.ValidationMessage = "Please eneter a username and password";
                return Page();
            }
            if (_context.User.Any(x => x.UserName == NewUser.UserName))//Checks if the username is already taken
            {
                NewUser.ValidationMessage = "Username already exists!";
                return Page();
            }
            CreatePasswordHash(NewUser.Password, out byte[] passwordHash, out byte[] passwordSalt);//Takes in the password and creates the hash and salt

            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = NewUser.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Admin = NewUser.Admin
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
