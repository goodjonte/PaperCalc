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
    public class FlatModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public FlatModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }

        public void OnGetAsync()
        {
            
        }

        public void OnPost()
        {
           
        }
    }
}