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
    public class DocumentsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private readonly IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public DocumentsModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Flat).ToList();
            DocumentsStock = _context.DocumentsStock.ToList();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
            Inputs = new();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public PaperCalc.DTOs.DocumentFormInputs Inputs { get; set; }
        public PaperCalc.DTOs.DocumentCalculation? Calculation { get; set; } = null;
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.DocumentsStock> DocumentsStock { get; set; }

        public void OnGetAsync()
        {
            
        }

        public void OnPost()
        {
            Calculation = new(_context, _env.ContentRootPath, Inputs);
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Flat).ToList();
            DocumentsStock = _context.DocumentsStock.ToList();
        }
    }
}