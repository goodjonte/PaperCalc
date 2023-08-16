using System.ComponentModel.DataAnnotations;

namespace PaperCalc.DTOs
{
    public class AspeosStockCoatingsDTO
    {
        public Guid CoatingId { get; set; }
        public required string Name { get; set; }
        public List<Guid>? Guids { get; set; }
        [Display(Name="Average Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? AveragePrice { get; set; }
        [Display(Name = "Selected Stock Items")]
        public int? NumberOfPapers { get; set; }
    }
}
