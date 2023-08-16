﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.EpsonStock
{
    public class DeleteModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public DeleteModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PaperCalc.Models.EpsonStock EpsonStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.EpsonStock == null)
            {
                return NotFound();
            }

            var epsonstock = await _context.EpsonStock.FirstOrDefaultAsync(m => m.Id == id);

            if (epsonstock == null)
            {
                return NotFound();
            }
            else 
            {
                EpsonStock = epsonstock;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.EpsonStock == null)
            {
                return NotFound();
            }
            var epsonstock = await _context.EpsonStock.FindAsync(id);

            if (epsonstock != null)
            {
                EpsonStock = epsonstock;
                _context.EpsonStock.Remove(EpsonStock);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}