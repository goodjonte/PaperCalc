using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;
using PaperCalc.Operations;

namespace PaperCalc.Pages.Coatings
{
    public class DetailsModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DetailsModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public PaperCalc.Models.Coatings Coatings { get; set; } = default!;
        [BindProperty]
        public List<PaperCalc.Models.Paper> CoatingsPapers { get; set; } = new List<PaperCalc.Models.Paper>();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Coatings == null)
            {
                return NotFound();
            }

            ViewData["Average"] = CoatingsOperations.getAverage(_context, id);
            ViewData["NumberOfPapers"] = CoatingsOperations.getNumberOfPapers(_context, id);

            var coatings = await _context.Coatings.FirstOrDefaultAsync(m => m.Id == id);
            var coatingsPapers = _context.CoatingsPapers.Where(m => m.CoatingId == id);
            foreach (CoatingsPaper item in coatingsPapers)
            {
                CoatingsPapers.Add(_context.Paper.Find(item.PaperId));
            }
            if (coatings == null)
            {
                return NotFound();
            }
            else 
            {
                Coatings = coatings;
            }
            return Page();
        }
    }
}
