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

namespace PaperCalc.Pages.Stock.AspeosStockCoatings
{
    public class CreateModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public CreateModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            AspeosStock = _context.AspeosStock.ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PaperCalc.DTOs.AspeosStockCoatingsDTO AspeosStockCoatingsDTO { get; set; } = default!;
        [BindProperty]
        public List<PaperCalc.Models.AspeosStock> AspeosStock { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.AspeosStockCoatings == null || AspeosStockCoatingsDTO == null)
            {
                return Page();
            }

            Guid newCoatingGuid = Guid.NewGuid();
            PaperCalc.Models.AspeosStockCoatings newCoating = new()
            {
                Id = newCoatingGuid,
                Name = AspeosStockCoatingsDTO.Name
            };
            _context.AspeosStockCoatings.Add(newCoating);
            if(AspeosStockCoatingsDTO.Guids != null)
            {
                foreach (Guid paperId in AspeosStockCoatingsDTO.Guids)
                {
                    PaperCalc.Models.CoatingsPaper newCoatingPaper = new()
                    {
                        Id = Guid.NewGuid(),
                        CoatingId = newCoatingGuid,
                        PaperId = paperId
                    };
                    _context.CoatingsPapers.Add(newCoatingPaper);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
