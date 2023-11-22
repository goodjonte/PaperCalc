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
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Document).ToList();
            DocumentsStock = _context.DocumentsStock.ToList();
            Settings.SetSettings(_env.ContentRootPath);
            _configuration = config;
            Inputs = new();
            Jobs = _context.Job.ToList();
            //Sort list by newest
            Jobs.Sort((x, y) => y.Created.CompareTo(x.Created));
        }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        [BindProperty]
        public PaperCalc.Models.DocumentFormInputs Inputs { get; set; }
        public PaperCalc.DTOs.DocumentCalculation? Calculation { get; set; } = null;
        public List<PaperCalc.Models.FlatSize> FlatSizes { get; set; }
        public List<PaperCalc.Models.DocumentsStock> DocumentsStock { get; set; }
        [BindProperty]
        public List<Job> Jobs { get; set; }
        [BindProperty]
        public Job Job { get; set; }
        [BindProperty]
        public Guid? JobId { get; set; }
        public bool Admin { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var token = Request.Cookies["Parrot"];
            if (token == null) { return Redirect("/Login"); }

            if (PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                if (_context.Sra3AndBookletsStock != null)
                {
                    Inputs = new DocumentFormInputs();
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
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Document).ToList();
            DocumentsStock = _context.DocumentsStock.ToList();

            //Give inputs a id
            Inputs.Id = Guid.NewGuid();

            //Add quote to job
            if (JobId == null)
            {
                //Create new job
                Job newJob = new()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
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
                    CalculationType = CalculationType.Document
                };
                _context.InputsForJobs.Add(IFJ);

                //Add inputs
                _context.DocumentFormInputs.Add(Inputs);

                _context.SaveChanges();

                return RedirectToPage("./Jobs/Details", new { id = newJob.Id.ToString() });
            }

            //Add connection from inputs to job
            InputsForJobs newIFJ = new()
            {
                Id = Guid.NewGuid(),
                JobId = (Guid)JobId,
                InputId = Inputs.Id,
                CalculationType = CalculationType.Document
            };
            _context.InputsForJobs.Add(newIFJ);

            //Add inputs
            _context.DocumentFormInputs.Add(Inputs);

            _context.SaveChanges();

            return RedirectToPage("./Jobs/Details", new { id = JobId.ToString() });
        }
    }
}