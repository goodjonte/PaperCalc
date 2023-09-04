using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PaperCalc.Pages
{
    public class WideFormatModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public WideFormatModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
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
