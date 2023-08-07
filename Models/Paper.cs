using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Paper
    {
        [Key]
        public Guid Id { get; set; }
        public string Company { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Size { get; set; } = default!;
        public int Weight { get; set; } = default!;
        public PaperType Type { get; set; }
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCost { get; set; }
        [Display(Name = "Pack Cost (GST Incl)")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCostGstIncluded { get { return Math.Round(PackCost * 1.15, 2); } }
        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }
        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PackCost / SheetsPerPack, 2); } }
        
    }

    public enum PaperType
    {
        Adhesive,
        Coated,
        Uncoated,
        Synthetic
    }
}
