using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperCalc.Models;

namespace PaperCalc.Pages
{
    [BindProperties]
    public class BookletsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private readonly IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public BookletsModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
            BookletsStock = _context.Sra3AndBookletsStock.ToList();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
            Inputs = new();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public PaperCalc.DTOs.BookletFormInputs Inputs { get; set; }
        public PaperCalc.DTOs.BookletCalculation? Calculation { get; set; } = null;
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.Sra3AndBookletsStock> BookletsStock { get; set; }

        public void OnGetAsync()
        {

        }

        public void OnPost()
        {
            Calculation = new(_context, _env.ContentRootPath, Inputs);
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
            BookletsStock = _context.Sra3AndBookletsStock.ToList();
        }
    }
}
