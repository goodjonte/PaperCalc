using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperCalc.Models;

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
            Inputs = new();
            Jobs = _context.Job.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.WideFormat).ToList();
            Stock = _context.WideFormatStock.ToList();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        [BindProperty]
        public PaperCalc.Models.WideFormatFormInputs Inputs { get; set; }
        public PaperCalc.DTOs.WideFormatCalculation? Calculation { get; set; } = null;
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.WideFormatStock> Stock { get; set; }
        [BindProperty]
        public List<Job> Jobs { get; set; }
        [BindProperty]
        public Job Job { get; set; }
        [BindProperty]
        public Guid? JobId { get; set; }
        public bool Admin { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            var token = Request.Cookies["Parrot"];
            if (token == null) { return Redirect("/Login"); }

            if (PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                if (_context.WideFormatStock != null)
                {
                    Inputs = new WideFormatFormInputs();
                }
                Admin = PaperCalc.Models.User.IsAdmin(token);

                if (id != null)
                {
                    JobId = id;
                }

                return Page();
            }
            else
            {
                return Redirect("/Login");
            }
        }

        public IActionResult OnPost()
        {
            Calculation = new(_context, _env.ContentRootPath, Inputs);
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.WideFormat).ToList();
            Stock = _context.WideFormatStock.ToList();

            //Give inputs a id
            Inputs.Id = Guid.NewGuid();

            //Add quote to job
            if (JobId == null)
            {
                //Create new job
                Job newJob = new()
                {
                    Id = Guid.NewGuid(),
                    JobTitle = Job.JobTitle,
                    ClientName = Job.ClientName,
                    Buissnessname = Job.Buissnessname
                };
                _context.Job.Add(newJob);

                //Add connection from inputs to job
                InputsForJobs IFJ = new()
                {
                    Id = Guid.NewGuid(),
                    JobId = newJob.Id,
                    InputId = Inputs.Id,
                    CalculationType = CalculationType.WideFormat
                };
                _context.InputsForJobs.Add(IFJ);

                //Add inputs
                _context.WideFormatFormInputs.Add(Inputs);

                _context.SaveChanges();

                return RedirectToPage("./Jobs/Details", new { id = newJob.Id.ToString() });
            }

            //Add connection from inputs to job
            InputsForJobs newIFJ = new()
            {
                Id = Guid.NewGuid(),
                JobId = (Guid)JobId,
                InputId = Inputs.Id,
                CalculationType = CalculationType.WideFormat
            };
            _context.InputsForJobs.Add(newIFJ);

            //Add inputs
            _context.WideFormatFormInputs.Add(Inputs);

            _context.SaveChanges();

            return RedirectToPage("./Jobs/Details", new { id = JobId.ToString() });
        }
    }
}
