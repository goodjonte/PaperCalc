using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperCalc.Models;

namespace PaperCalc.Pages
{
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
            Jobs = _context.Job.ToList();
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        [BindProperty]
        public PaperCalc.Models.BookletFormInputs Inputs { get; set; }
        public PaperCalc.DTOs.BookletCalculation? Calculation { get; set; } = null;
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.Sra3AndBookletsStock> BookletsStock { get; set; }
        [BindProperty]
        public List<Job> Jobs { get; set; }
        [BindProperty]
        public Job Job { get; set; }
        [BindProperty]
        public Guid? JobId { get; set; }

        public void OnGetAsync(Guid? id)
        {
            if (id != null)
            {
                JobId = id;
            }

        }

        public IActionResult OnPost()
        {
            Calculation = new(_context, _env.ContentRootPath, Inputs);
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
            BookletsStock = _context.Sra3AndBookletsStock.ToList();


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
                    CalculationType = CalculationType.Booklet
                };
                _context.InputsForJobs.Add(IFJ);

                //Add inputs
                _context.BookletFormInputs.Add(Inputs);

                _context.SaveChanges();

                return RedirectToPage("./Jobs/Details", new { id = newJob.Id.ToString() });
            }

            //Add connection from inputs to job
            InputsForJobs newIFJ = new()
            {
                Id = Guid.NewGuid(),
                JobId = (Guid)JobId,
                InputId = Inputs.Id,
                CalculationType = CalculationType.Booklet
            };
            _context.InputsForJobs.Add(newIFJ);

            //Add inputs
            _context.BookletFormInputs.Add(Inputs);

            _context.SaveChanges();

            return RedirectToPage("./Jobs/Details", new { id = JobId.ToString() });
        }
    }
}
