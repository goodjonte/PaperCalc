using PaperCalc.DTOs;

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

        //DTO
        public JobType JobTypeForDTO { get; set; }
        public AspeosCalculation? AspeosCalculation { get; set; }
        public BookletCalculation? BookletCalculation { get; set; }
        public WideFormatCalculation? WideFormatCalculation { get; set; }
        public FlatCalculation? FlatCalculation { get; set; }

        //Values for DTOS
        public double? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
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

        public void SetDTO()
        {
            switch (JobTypeForDTO)
            {
                case JobType.SRA3:
                    AspeosCalculation = new AspeosCalculation
                    {
                        Quantity = Quantity,
                        FlatSizeId = FlatSizeId,
                        CoatType = CoatType,
                        CustomSize = CustomSize,
                        Height = Height,
                        Width = Width,
                        Colour = Colour,
                        PrintedSides = PrintedSides,
                        NumOfHolePunches = NumOfHolePunches,
                        Trimming = Trimming,
                        Lamination = Lamination,
                        SmallJob = SmallJob,
                        Urgent = Urgent,
                        FileHandlingFee = FileHandlingFee,
                        Creasing = Creasing,
                        Folds = Folds,
                    };
                    break;
                case JobType.Flat:
                    FlatCalculation = new FlatCalculation
                    {
                        Pages = Pages,
                        CopyQuantity = CopyQuantity,
                        FlatSizeId = FlatSizeId,
                        Colour = Colour,
                        PrintedSides = PrintedSides,
                        Binding = Binding,
                        NumOfHolePunches = NumOfHolePunches,
                        Lamination = Lamination,
                        Creasing = Creasing,
                        Folds = Folds,
                        Urgent = Urgent,
                        FileHandlingFee = FileHandlingFee,
                    };
                    break;
                case JobType.Booklet:
                    BookletCalculation = new BookletCalculation
                    {
                        TotalPages = Pages,
                        CopyQuantity = CopyQuantity,
                        FlatSizeId = FlatSizeId,
                        Colour = Colour,
                        PrintedSides = PrintedSides,
                        Trimming = Trimming,
                        NumOfHolePunches = NumOfHolePunches,
                        Lamination = Lamination,
                        SmallJob = SmallJob,
                        Urgent = Urgent,
                        FileHandlingFee = FileHandlingFee,
                        Creasing = Creasing,
                        Folds = Folds,
                    };
                    break;
                case JobType.WideFormat:
                    break;
                default:
                    break;
            }
        }
    }
    public enum JobType
    {
        SRA3 = 0,
        Flat = 1,
        Booklet = 2,
        WideFormat = 3,
    }
}
