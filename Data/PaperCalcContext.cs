using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaperCalc.Models;

namespace PaperCalc.Data
{
    public class PaperCalcContext : DbContext
    {
        public PaperCalcContext (DbContextOptions<PaperCalcContext> options)
            : base(options)
        {
        }

        public DbSet<PaperCalc.Models.Paper> Paper { get; set; } = default!;

        public DbSet<PaperCalc.Models.Coatings> Coatings { get; set; } = default!;
        public DbSet<PaperCalc.Models.CoatingsPaper> CoatingsPapers { get; set; } = default!;

    }
}
