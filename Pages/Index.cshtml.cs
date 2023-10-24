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
        private readonly IWebHostEnvironment _env;
        public readonly IConfiguration _configuration;

        public IndexModel(IConfiguration config, PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            Settings = new();
            Settings.SetSettings(_env.ContentRootPath);
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Sra3).ToList();
            _configuration = config;
            Inputs = new();
            Calculation = new(_context, _env.ContentRootPath, Inputs);
            Jobs = _context.Job.ToList();
        }
        public bool Admin { get; set; }
        public PaperCalc.DTOs.Settings? Settings { get; set; }
        public IList<PaperCalc.Models.Sra3AndBookletsStock> Paper { get; set; } = default!;

        public IList<PaperCalc.Models.FlatSize> FlatSizes { get; set; } = default!;
        [BindProperty]
        public Sra3FormInput Inputs { get; set; }
        [BindProperty]
        public List<Job> Jobs { get; set; }
        [BindProperty]
        public Job Job { get; set; }
        [BindProperty]
        public Guid? JobId { get; set; }
        public Sra3Calculation Calculation { get; set; }

        public IActionResult OnGet(Guid? id)
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

                if(id != null)
                {
                    JobId = id;
                }


                return Page();
            }else
            {
                return Redirect("/Login");
            }
        }

        public IActionResult OnPost()
        {
            //Auth user
            var token = Request.Cookies["Parrot"];
            if (token == null) { return NotFound(); }
            if (PaperCalc.Models.User.VerifyToken(_configuration, token))
            {
                Admin = PaperCalc.Models.User.IsAdmin(token);
            }

            //Make Calculation
            Calculation = new Sra3Calculation(_context, _env.ContentRootPath, Inputs);

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
                    CalculationType = CalculationType.Sra3
                };
                _context.InputsForJobs.Add(IFJ);

                //Add inputs
                _context.Sra3FormInput.Add(Inputs);

                _context.SaveChanges();

                return RedirectToPage("./Jobs/Details", new { id = newJob.Id.ToString() });
            }

            //Add connection from inputs to job
            InputsForJobs newIFJ = new()
            {
                Id = Guid.NewGuid(),
                JobId = (Guid)JobId,
                InputId = Inputs.Id,
                CalculationType = CalculationType.Sra3
            };
            _context.InputsForJobs.Add(newIFJ);

            //Add inputs
            _context.Sra3FormInput.Add(Inputs);

            _context.SaveChanges();

            return RedirectToPage("./Jobs/Details", new { id = JobId.ToString() });
        }
    }
}