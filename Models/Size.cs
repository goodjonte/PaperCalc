using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Size
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } //e.g. SRA4
        public string? Format { get; set; } //e.g. SRA
        public int? FormatNumber { get; set; } //e.g. 4
        [Display(Name = "Width (Portrait)")]
        public int? XAxis { get; set; } // e.g. 225 (mm)
        [Display(Name = "Height (Portrait)")]
        public int? YAxis { get; set; } // e.g. 320 (mm)
    }
}
