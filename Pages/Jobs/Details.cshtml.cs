using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;
        private readonly IWebHostEnvironment _env;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [BindProperty]
        public Job Job { get; set; } = default!;
        [BindProperty]
        public int numberOfProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Job == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            else
            {
                Job = job;
                Job.GetItems(_context, _env.ContentRootPath);
            }

            numberOfProducts = Job.Sra3Items.Count > 0 ? 1 : 0;
            numberOfProducts = Job.BookletItems.Count > 0 ? numberOfProducts + 1 : numberOfProducts;
            numberOfProducts = Job.DocumentItems.Count > 0 ? numberOfProducts + 1 : numberOfProducts;
            numberOfProducts = Job.WideFormatItems.Count > 0 ? numberOfProducts + 1 : numberOfProducts;

            return Page();
        }

        public async Task<ActionResult> OnPostSaveJobDetails()
        {
            _context.Attach(Job).State = EntityState.Modified;

            _context.SaveChanges();

            return RedirectToPage("./Details", new { id = Job.Id.ToString() });
        }
    }
}
