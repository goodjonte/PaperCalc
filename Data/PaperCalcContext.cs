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
        public DbSet<PaperCalc.Models.Sra3AndBookletsStock> Sra3AndBookletsStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.DocumentsStock> DocumentsStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.BindingCoverStock> BindingCoverStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.BindingCoilsStock> BindingCoilsStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.WideFormatStock> WideFormatStock { get; set; } = default!;
        public DbSet<PaperCalc.Models.User> User { get; set; } = default!;
        public DbSet<PaperCalc.Models.Job> Job { get; set; } = default!;
        public DbSet<PaperCalc.Models.InputsForJobs> InputsForJobs { get; set; } = default!;
        public DbSet<PaperCalc.Models.Sra3FormInput> Sra3FormInput { get; set; } = default!;
        public DbSet<PaperCalc.Models.BookletFormInputs> BookletFormInputs { get; set; } = default!;
        public DbSet<PaperCalc.Models.DocumentFormInputs> DocumentFormInputs { get; set; } = default!;

    }
}
