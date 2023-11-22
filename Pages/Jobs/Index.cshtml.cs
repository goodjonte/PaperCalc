using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.Pages.Jobs
{
    public class IndexModel : PageModel
    {
        private readonly PaperCalc.Data.PaperCalcContext _context;

        public IndexModel(PaperCalc.Data.PaperCalcContext context)
        {
            _context = context;
        }

        public List<Job> Jobs { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Job != null)
            {
                Jobs = await _context.Job.ToListAsync();

                //Sort list by newest
                Jobs.Sort((x, y) => y.Created.CompareTo(x.Created));
            }
        }
    }
}
