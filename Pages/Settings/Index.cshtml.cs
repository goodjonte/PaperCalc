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
            AspeosFlatSizes = new();
            FlatFlatSizes = new();
            BookletFlatSizes = new();
            _env = env;
            Settings = new();
            NewUser = new();
            AspeosFlatSize = new()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };
            FlatFlatSize = new()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };
            BookletFlatSize = new()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };
            _configuration = config;
        }
        [BindProperty]
        public PaperCalc.DTOs.NewUserDTO NewUser { get; set; }
        [BindProperty]
        public PaperCalc.DTOs.Settings Settings { get; set; }
        [BindProperty]
        public List<PaperCalc.Models.AspeosFlatSize> AspeosFlatSizes { get; set; }
        [BindProperty]
        public PaperCalc.Models.AspeosFlatSize AspeosFlatSize { get; set; }
        [BindProperty]
        public List<PaperCalc.Models.FlatFlatSize> FlatFlatSizes { get; set; }
        [BindProperty]
        public PaperCalc.Models.FlatFlatSize FlatFlatSize { get; set; }
        [BindProperty]
        public List<PaperCalc.Models.BookletFlatSize> BookletFlatSizes { get; set; }
        [BindProperty]
        public PaperCalc.Models.BookletFlatSize BookletFlatSize { get; set; }

        //GET Method
        public IActionResult OnGet()
        {
            Settings.SetSettings(_env.ContentRootPath);

            var token = Request.Cookies["Parrot"];
            if (token == null || !PaperCalc.Models.User.VerifyToken(_configuration, token) || !PaperCalc.Models.User.IsAdmin(token))
            {
                return Redirect("/Login");
            }

            AspeosFlatSizes = _context.AspeosFlatSizes.ToList();
            FlatFlatSizes = _context.FlatFlatSizes.ToList();
            BookletFlatSizes = _context.BookletFlatSizes.ToList();
            return Page();
        }

        //Post Methods
        public ActionResult OnPostSaveSettings()
        {
            Settings.SaveSettings(_env.ContentRootPath);
            return RedirectToPage("./Index");
        }
        //Aspeos Flat size methods
        public ActionResult OnPostAspeosCreate()
        {
            if(AspeosFlatSize == null)
            {
                return Page();
            }

            _context.AspeosFlatSizes.Add(AspeosFlatSize);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        public ActionResult OnPostAspeosEdit()
        {
            if(AspeosFlatSize == null)
            {
                return Page();
            }
            if(string.Equals(AspeosFlatSize.Name, "delete", StringComparison.OrdinalIgnoreCase))
            {
                _context.AspeosFlatSizes.Remove(AspeosFlatSize);
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }
            
            _context.Attach(AspeosFlatSize).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        //Flat - Flat size methods
        public ActionResult OnPostFlatCreate()
        {
            if (FlatFlatSize == null)
            {
                return Page();
            }

            _context.FlatFlatSizes.Add(FlatFlatSize);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        public ActionResult OnPostFlatEdit()
        {
            if (FlatFlatSize == null)
            {
                return Page();
            }
            if (string.Equals(FlatFlatSize.Name, "delete", StringComparison.OrdinalIgnoreCase))
            {
                _context.FlatFlatSizes.Remove(FlatFlatSize);
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }

            _context.Attach(FlatFlatSize).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        //Booklet - Flat size methods
        public ActionResult OnPostBookletCreate()
        {
            if (BookletFlatSize == null)
            {
                return Page();
            }

            _context.BookletFlatSizes.Add(BookletFlatSize);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
        public ActionResult OnPostBookletEdit()
        {
            if (BookletFlatSize == null)
            {
                return Page();
            }
            if (string.Equals(BookletFlatSize.Name, "delete", StringComparison.OrdinalIgnoreCase))
            {
                _context.BookletFlatSizes.Remove(BookletFlatSize);
                _context.SaveChanges();
                return RedirectToPage("./Index");
            }

            _context.Attach(BookletFlatSize).State = EntityState.Modified;
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
