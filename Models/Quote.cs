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
        public FlatCalculation? FlatCalculation { get; set; }

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

        public void SetDTO()
        {
            switch (JobTypeForDTO)
            {
                case JobType.SRA3:
                    AspeosCalculation = new Sra3Calculation
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
                        FlatSizeHW = FlatSizeHW,
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

        //Values for quote from calculation
        public void SetQuoteDTOValues(Sra3Calculation asp)
        {
            Quantity = asp?.Quantity;
            FlatSizeId = asp?.FlatSizeId;
            CoatType = asp?.CoatType;
            CustomSize = asp?.CustomSize ?? false;
            Height = asp?.Height ?? 0;
            Width = asp?.Width ?? 0;
            Colour = asp?.Colour;
            PrintedSides = asp?.PrintedSides;
            NumOfHolePunches = asp?.NumOfHolePunches ?? 0;
            Trimming = asp?.Trimming ?? false;
            Lamination = asp?.Lamination ?? false;
            SmallJob = asp?.SmallJob ?? false;
            Urgent = asp?.Urgent ?? false;
            FileHandlingFee = asp?.FileHandlingFee;
            Creasing = asp?.Creasing ?? 0;
            Folds = asp?.Folds ?? 0;
        }
        public void SetQuoteDTOValues(FlatCalculation flat)
        {
            Pages = flat.Pages;
            CopyQuantity = flat.CopyQuantity;
            FlatSizeHW = flat.FlatSizeHW;
            Colour = flat.Colour;
            PrintedSides = flat.PrintedSides;
            Binding = flat.Binding;
            NumOfHolePunches = flat.NumOfHolePunches;
            Lamination = flat.Lamination;
            Creasing = flat.Creasing;
            Folds = flat.Folds;
            Urgent = flat.Urgent;
            FileHandlingFee = flat.FileHandlingFee;
        }
        public void SetQuoteDTOValues(BookletCalculation book)
        {
            Pages = book.TotalPages;
            CopyQuantity = book.CopyQuantity;
            FlatSizeId = book.FlatSizeId;
            Colour = book.Colour;
            PrintedSides = book.PrintedSides;
            Trimming = book.Trimming;
            NumOfHolePunches = book.NumOfHolePunches;
            Lamination = book.Lamination;
            SmallJob = book.SmallJob;
            Urgent = book.Urgent;
            FileHandlingFee = book.FileHandlingFee;
            Creasing = book.Creasing;
            Folds = book.Folds;
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
