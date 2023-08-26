using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using PaperCalc.DTOs;
//using PaperCalc.Data;
using PaperCalc.Models;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PaperCalc.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public IndexModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            Settings.SetSettings(_env.ContentRootPath);
            Paper = _context.AspeosStock.ToList();
            _configuration = config;
        }
        public bool Admin { get; set; }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public IList<PaperCalc.Models.AspeosStock> Paper { get; set; } = default!;

        public IList<PaperCalc.Models.AspeosFlatSize> FlatSize { get; set; } = default!;
        public AspeosCalculation? AspeosCalculation { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["Parrot"];
            if (token == null) { return Redirect("/Login");}

            if(PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                if (_context.AspeosStock != null)
                {
                    AspeosCalculation = new AspeosCalculation();
                    FlatSize = await _context.AspeosFlatSizes.ToListAsync();
                }
                Admin = PaperCalc.Models.User.IsAdmin(token);
                return Page();
            }else
            {
                return Redirect("/Login");
            }
        }

        public void OnPost()
        {
            if(AspeosCalculation != null)
            {
                AspeosCalculation.Calculate(_context, _env.ContentRootPath);
                FlatSize = _context.AspeosFlatSizes.ToList();
                Paper = _context.AspeosStock.ToList();
            }
            if(AspeosCalculation != null && AspeosCalculation.FileHandlingFee != null)
            {
                Settings = new();
                Settings.FilehandlingCost = (double)AspeosCalculation.FileHandlingFee;
            }
        }
    }
}