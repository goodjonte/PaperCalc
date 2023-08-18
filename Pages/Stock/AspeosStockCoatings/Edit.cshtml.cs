using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Stock.AspeosStockCoatings
{
    public class EditModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public EditModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
            AspeosStock = _context.AspeosStock.ToList();
        }

        [BindProperty]
        public PaperCalc.DTOs.AspeosStockCoatingsDTO AspeosStockCoatingsDTO { get; set; } = default!;
        [BindProperty]
        public List<PaperCalc.Models.AspeosStock> AspeosStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var cookieValue = Request.Cookies["PaperCalc"];
            if (cookieValue == null || !PaperCalc.Models.Login.ValidatePassword(_context, cookieValue))
            {
                return Redirect("/Login");
            }
            if (id == null || _context.AspeosStockCoatings == null)
            {
                return NotFound();
            }

            var aspeosstockcoatings =  await _context.AspeosStockCoatings.FirstOrDefaultAsync(m => m.Id == id);
            var aspeosstockcoatingsPapers = _context.CoatingsPapers.Where(m => m.CoatingId == id);
            if (aspeosstockcoatings == null)
            {
                return NotFound();
            }

            List<Guid> guidList = new();
            foreach(CoatingsPaper cp in aspeosstockcoatingsPapers)
            {
                guidList.Add(cp.PaperId);
            }

            AspeosStockCoatingsDTO = new()
            {
                Name = aspeosstockcoatings.Name,
                CoatingId = aspeosstockcoatings.Id,
                Guids = guidList
            };

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

            //Update the coating name
            PaperCalc.Models.AspeosStockCoatings editedCoating = new()
            {
                Id = AspeosStockCoatingsDTO.CoatingId,
                Name = AspeosStockCoatingsDTO.Name
            };
            _context.Attach(editedCoating).State = EntityState.Modified;

            //Delete all the old papers
            _context.CoatingsPapers.RemoveRange(_context.CoatingsPapers.Where(m => m.CoatingId == editedCoating.Id));
            //Add all the new papers
            if(AspeosStockCoatingsDTO.Guids != null)
            {
                foreach (Guid pId in AspeosStockCoatingsDTO.Guids)
                {
                    CoatingsPaper cp = new()
                    {
                        Id = Guid.NewGuid(),
                        PaperId = pId,
                        CoatingId = editedCoating.Id
                    };
                    _context.CoatingsPapers.Add(cp);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AspeosStockCoatingsExists(AspeosStockCoatingsDTO.CoatingId))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        private bool AspeosStockCoatingsExists(Guid id)
        {
          return (_context.AspeosStockCoatings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
