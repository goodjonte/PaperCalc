using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using PaperCalc.DTOs;
using PaperCalc.Models;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace PaperCalc.Pages
{
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
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Aspeos).ToList();
            _configuration = config;
            Inputs = new();
            Calculation = new(_context, _env.ContentRootPath, Inputs);
        }
        public bool Admin { get; set; }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public IList<PaperCalc.Models.Sra3AndBookletsStock> Paper { get; set; } = default!;

        public IList<PaperCalc.Models.FlatSize> FlatSizes { get; set; } = default!;
        [BindProperty]
        public Sra3FormInput Inputs { get; set; }
        public Sra3Calculation Calculation { get; set; }
        public Quote Quote { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            var token = Request.Cookies["Parrot"];
            if (token == null) { return Redirect("/Login");}

            if(PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                if (_context.Sra3AndBookletsStock != null)
                {
                    Inputs = new Sra3FormInput();
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
            
            Calculation = new Sra3Calculation(_context, _env.ContentRootPath, Inputs);

            //Auth user
            var token = Request.Cookies["Parrot"];
            if (token == null) { return; }
            if (PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                Admin = PaperCalc.Models.User.IsAdmin(token);
            }
        }
    }
}