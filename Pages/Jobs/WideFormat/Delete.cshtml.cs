using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.WideFormat
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WideFormatFormInputs WideFormatFormInputs { get; set; } = default!;
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == WideFormatFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.WideFormatFormInputs == null)
            {
                return NotFound();
            }

            var wideforminput = await _context.WideFormatFormInputs.FirstOrDefaultAsync(m => m.Id == id);

            if (wideforminput == null)
            {
                return NotFound();
            }
            else
            {
                WideFormatFormInputs = wideforminput;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.WideFormatFormInputs == null)
            {
                return NotFound();
            }
            var wideFormatFormInputs = await _context.WideFormatFormInputs.FindAsync(id);

            if (wideFormatFormInputs != null)
            {
                WideFormatFormInputs = wideFormatFormInputs;
                _context.WideFormatFormInputs.Remove(WideFormatFormInputs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Details", new { id = Connection.JobId.ToString() });
        }
    }
}
