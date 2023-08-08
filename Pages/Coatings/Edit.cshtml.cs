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

namespace PaperCalc.Pages.Coatings
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            papers = _context.Paper.ToList();
        }

        [BindProperty]
        public CoatingsDTO Coatings { get; set; } = new CoatingsDTO();
        [BindProperty]
        public List<PaperCalc.Models.Paper> papers { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Coatings == null)
            {
                return NotFound();
            }

            var coatings =  await _context.Coatings.FirstOrDefaultAsync(m => m.Id == id);
            var coatingsPapers = _context.CoatingsPapers.Where(m => m.CoatingId == id);
            if (coatings == null)
            {
                return NotFound();
            }
            Coatings.CoatingId = coatings.Id;
            Coatings.Name = coatings.Name;
            List<Guid> paperIds = new List<Guid>();

            foreach(PaperCalc.Models.CoatingsPaper cp in coatingsPapers)
            {
                paperIds.Add(cp.PaperId);
            }

            Coatings.guids = paperIds;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            PaperCalc.Models.Coatings editedCoating = new PaperCalc.Models.Coatings();
            editedCoating.Id = Coatings.CoatingId;
            editedCoating.Name = Coatings.Name;

            _context.Attach(editedCoating).State = EntityState.Modified;

            _context.CoatingsPapers.RemoveRange(_context.CoatingsPapers.Where(m => m.CoatingId == editedCoating.Id));

            foreach (Guid pId in Coatings.guids)
            {
                CoatingsPaper cp = new CoatingsPaper();
                cp.Id = Guid.NewGuid();
                cp.PaperId = pId;
                cp.CoatingId = Coatings.CoatingId;
                _context.CoatingsPapers.Add(cp);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./index");

        }

        private bool CoatingsExists(Guid id)
        {
          return (_context.Coatings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
