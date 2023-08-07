using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Clicks
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Cost { get; set; }
        [Display(Name = "Colour")]
        public Guid? ColourId { get; set; }
        [Display(Name = "Size")]
        public Guid? SizeId { get; set; }
    }
}
