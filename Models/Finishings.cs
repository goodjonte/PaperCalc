using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Finishings
    {
        public Guid Id { get; set; }
        public string FinishingName { get; set; }
        public double FinishingCostPerItem { get; set; }
    }
}
