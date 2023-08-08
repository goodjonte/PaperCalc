using System.ComponentModel.DataAnnotations;

namespace PaperCalc.DTOs
{
    public class CoatingsDTO
    {
        public Guid CoatingId { get; set; }
        public string? Name { get; set; }
        public List<Guid>? guids { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? AveragePrice { get; set; }
        public int? NumberOfPapers { get; set; }
    }
}
