using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.Sra3
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Sra3FormInput Sra3FormInput { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Sra3FormInput == null)
            {
                return NotFound();
            }

            var sra3forminput = await _context.Sra3FormInput.FirstOrDefaultAsync(m => m.Id == id);

            if (sra3forminput == null)
            {
                return NotFound();
            }
            else 
            {
                Sra3FormInput = sra3forminput;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Sra3FormInput == null)
            {
                return NotFound();
            }
            var sra3forminput = await _context.Sra3FormInput.FindAsync(id);

            if (sra3forminput != null)
            {
                Sra3FormInput = sra3forminput;
                _context.Sra3FormInput.Remove(Sra3FormInput);
                await _context.SaveChangesAsync();
            }
            InputsForJobs connection = _context.InputsForJobs.Where(x => x.InputId == Sra3FormInput.Id).First();

            return RedirectToPage("../Details", new { id = connection.JobId.ToString() });
        }
    }
}
