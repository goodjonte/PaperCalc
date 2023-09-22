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
            AspeosStock = _context.AspeosStock.ToList();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
            BookletCalculation = new();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public PaperCalc.DTOs.BookletCalculation BookletCalculation { get; set; }
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.AspeosStock> AspeosStock { get; set; }

        public void OnGetAsync()
        {

        }

        public void OnPost()
        {
            BookletCalculation.Calculate(_context, _env.ContentRootPath);
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
            AspeosStock = _context.AspeosStock.ToList();
        }
    }
}
