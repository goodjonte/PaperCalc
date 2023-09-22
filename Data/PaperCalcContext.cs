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
        public DbSet<PaperCalc.Models.FlatSize> FlatSizes { get; set; } = default!;
        public DbSet<PaperCalc.Models.AspeosStock> AspeosStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.EpsonStock> EpsonStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.FlatStock> FlatStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.LaminationStock> LaminationStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.User> User { get; set; } = default!;
    }
}
