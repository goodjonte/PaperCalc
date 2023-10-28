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

namespace PaperCalc.Pages.Jobs.Booklet
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            Paper = _context.Sra3AndBookletsStock.ToList();
            FlatSizes = _context.FlatSizes.Where(x => x.ForCalculation == CalculationType.Booklet).ToList();
        }

        [BindProperty]
        public BookletFormInputs BookletFormInputs { get; set; } = default!;
        public List<Models.Sra3AndBookletsStock> Paper { get; set; }
        public List<FlatSize> FlatSizes { get; set; }
        [BindProperty]
        public InputsForJobs Connection { get { return _context.InputsForJobs.Where(x => x.InputId == BookletFormInputs.Id).First(); } }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.BookletFormInputs == null)
            {
                return NotFound();
            }

            var bookletFormInputs =  await _context.BookletFormInputs.FirstOrDefaultAsync(m => m.Id == id);
            if (bookletFormInputs == null)
            {
                return NotFound();
            }
            BookletFormInputs = bookletFormInputs;
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

            _context.Attach(BookletFormInputs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookletFormInputsExists(BookletFormInputs.Id))
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

        private bool BookletFormInputsExists(Guid id)
        {
          return (_context.BookletFormInputs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
