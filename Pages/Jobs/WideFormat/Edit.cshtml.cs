using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.DTOs;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs.WideFormat
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            Paper = _context.WideFormatStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.WideFormat).ToList();
        }

        [BindProperty]
        public WideFormatFormInputs WideFormatFormInputs { get; set; } = default!;
        public List<WideFormatStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == WideFormatFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.WideFormatFormInputs == null)
            {
                return NotFound();
            }

            var wideFormatFormInputs =  await _context.WideFormatFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (wideFormatFormInputs == null)
            {
                return NotFound();
            }
            WideFormatFormInputs = wideFormatFormInputs;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WideFormatFormInputs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WideFormatFormInputsExists(WideFormatFormInputs.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Details", new { id = Connection.JobId.ToString() });
        }

        private bool WideFormatFormInputsExists(Guid id)
        {
          return (_context.WideFormatFormInputs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
