using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaperCalc.Data;
using PaperCalc.DTOs;
using PaperCalc.Models;

namespace PaperCalc.Pages.Coatings
{
    public class CreateModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public CreateModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            papers = _context.Paper.ToList();
        }
        [BindProperty]
        public List<PaperCalc.Models.Paper> papers { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CoatingsDTO Coatings { get; set; } = default!;
        public PaperCalc.Models.Coatings coating { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Coatings == null || Coatings == null)
            {
                return Page();
            }
            Guid thisCoatingsId = Guid.NewGuid();
            coating = new PaperCalc.Models.Coatings();
            coating.Name = Coatings.Name;
            coating.Id = thisCoatingsId;
            _context.Coatings.Add(coating);
            

            foreach(Guid pId in Coatings.guids)
            {
                CoatingsPaper cp = new CoatingsPaper();
                cp.Id = Guid.NewGuid();
                cp.PaperId = pId;
                cp.CoatingId = thisCoatingsId;
                _context.CoatingsPapers.Add(cp);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
