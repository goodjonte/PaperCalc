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

namespace PaperCalc.Pages.Jobs.Sra3
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Sra3).ToList();
        }

        [BindProperty]
        public Sra3FormInput Sra3FormInput { get; set; } = default!;
        public List<Sra3AndBookletsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Sra3FormInput == null)
            {
                return NotFound();
            }

            var sra3forminput =  await _context.Sra3FormInput.FirstOrDefaultAsync(m => m.Id == id);
            if (sra3forminput == null)
            {
                return NotFound();
            }
            Sra3FormInput = sra3forminput;
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

            _context.Attach(Sra3FormInput).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Sra3FormInputExists(Sra3FormInput.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            InputsForJobs connection = _context.InputsForJobs.Where(x => x.InputId == Sra3FormInput.Id).First();

            return RedirectToPage("../Details", new { id = connection.JobId.ToString() });
        }

        private bool Sra3FormInputExists(Guid id)
        {
          return (_context.Sra3FormInput?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
