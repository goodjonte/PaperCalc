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
        public DbSet<PaperCalc.Models.AspeosStockCoatings> AspeosStockCoatings { get; set; } = default!;
        public DbSet<PaperCalc.Models.CoatingsPaper> CoatingsPapers { get; set; } = default!;
        public DbSet<PaperCalc.Models.AspeosFlatSize> AspeosFlatSizes { get; set; } = default!;
        public DbSet<PaperCalc.Models.EpsonFlatSize> EpsonFlatSizes { get; set; } = default!;
        public DbSet<PaperCalc.Models.AspeosStock> AspeosStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.EpsonStock> EpsonStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.Login> Login { get; set; } = default!;
    }
}
