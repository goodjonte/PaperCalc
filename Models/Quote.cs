using PaperCalc.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaperCalc.Models
{
    public class Quote
    {
        public Guid Id { get; set; }
        public string? JobTitle { get; set; }
        public string? ClientName { get; set; }
        public string? BuissnessName { get; set; }
        public string? Address { get; set; }
        public double? Turnaround { get; set; }
        public double? DeliveryCost { get; set; }
        public double? DesignCost { get; set; }
        public double? SetupCost { get; set; }
        public JobType JobTypeForDTO { get; set; }

        //DTO - not saved in db
        [NotMapped]
        public bool save { get; set; }
        [NotMapped]
        public Sra3Calculation? AspeosCalculation { get; set; }
        [NotMapped]
        public BookletCalculation? BookletCalculation { get; set; }
        [NotMapped]
        public WideFormatCalculation? WideFormatCalculation { get; set; }
        [NotMapped]
        public DocumentCalculation? FlatCalculation { get; set; }

        //Values for DTOS
        public double? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public string? FlatSizeHW { get; set;}
        public CoatType? CoatType { get; set; }
        public bool CustomSize { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string? Colour { get; set; }
        public string? PrintedSides { get; set; }
        public int NumOfHolePunches { get; set; }
        public bool Trimming { get; set; }
        public bool Lamination { get; set; }
        public bool SmallJob { get; set; }
        public bool Urgent { get; set; }
        public double? FileHandlingFee { get; set; }
        public int Creasing { get; set; }
        public int Folds { get; set; }
        public int? Pages { get; set; }
        public int? CopyQuantity { get; set; }
        public bool Binding { get; set; }

        
    }
    public enum JobType
    {
        SRA3 = 0,
        Flat = 1,
        Booklet = 2,
        WideFormat = 3,
    }
}
