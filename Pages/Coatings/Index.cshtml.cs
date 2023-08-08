using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.DTOs;
using PaperCalc.Models;
using PaperCalc.Operations;

namespace PaperCalc.Pages.Coatings
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public IList<PaperCalc.Models.Coatings> Coatings { get;set; } = default!;
        [BindProperty]
        public List<CoatingsDTO> CoatingsDTOs { get; set; } = new List<CoatingsDTO>();

        public async Task OnGetAsync()
        {
            if (_context.Coatings != null)
            {
                Coatings = await _context.Coatings.ToListAsync();
                foreach(var coat in Coatings)
                {
                    CoatingsDTO cd = new CoatingsDTO();
                    cd.CoatingId = coat.Id;
                    cd.Name = coat.Name;
                    cd.AveragePrice = CoatingsOperations.getAverage(_context, coat.Id);
                    cd.NumberOfPapers = CoatingsOperations.getNumberOfPapers(_context, coat.Id);
                    CoatingsDTOs.Add(cd);
                }
            }
        }
    }
}
