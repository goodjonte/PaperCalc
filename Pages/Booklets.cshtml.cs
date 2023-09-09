using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            BookletFlatSizes = _context.BookletFlatSizes.ToList();
            AspeosStock = _context.AspeosStock.ToList();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
            BookletCalculation = new();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public PaperCalc.DTOs.BookletCalculation BookletCalculation { get; set; }
        public List<PaperCalc.Models.BookletFlatSize> BookletFlatSizes { get; set; }
        public List<PaperCalc.Models.AspeosStock> AspeosStock { get; set; }

        public void OnGetAsync()
        {

        }

        public void OnPost()
        {
            BookletCalculation.Calculate(_context, _env.ContentRootPath);
            BookletFlatSizes = _context.BookletFlatSizes.ToList();
            AspeosStock = _context.AspeosStock.ToList();
        }
    }
}
