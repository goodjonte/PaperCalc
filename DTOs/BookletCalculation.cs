using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class BookletCalculation
    {
        public Settings? Settings { get; set; }
        public int? TotalPages { get; set; }
        public int? CopyQuantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public string? Colour { get; set; }
        public string? PrintedSides { get; set; }
        public bool Trimming { get; set; }
        public int NumOfHolePunches { get; set; }
        public bool Lamination { get; set; }
        public int Creasing { get; set; }
        public int Folds { get; set; }
        public bool SmallJob { get; set; }
        public bool Urgent { get; set; }
        public bool FileHandling { get; set; }
        public double? FileHandlingFee { get; set; }

        //First row calcs
        public double? InnerSheetPrice { get; set; } //Set in front end
        public double? CoverSheetPrice { get; set; } //Set in front end
        public int? SheetsUsedInners { get { return ((TotalPages - 4) / 4) * CopyQuantity; } }
        public double? BaseClickRate { get { return Colour == "black" ? 0.01 : 0.08; } } //Hard coded in click rates
        public double? ClickSideMultiplier { get { return PrintedSides == "single" ? BaseClickRate * 1 : BaseClickRate * 2; } }
        public double? ClickSizeMultiplier { get { return Trimming ? ClickSideMultiplier * 2 : ClickSideMultiplier; } }
        public double? ClickRate { get { return ClickSizeMultiplier; } }
        public double? CutsCost { get { return SheetsUsedInners * 4 / 80; } }
        public double? HolePunchCost
        {
            get
            {
                return NumOfHolePunches switch
                {
                    0 => 0,
                    1 => TotalPages / 20 * 1,
                    2 => TotalPages / 20 * 2,
                    3 => TotalPages / 20 * 3,
                    _ => 0,
                };
            }
        }
        public double? FoldingCost { get { return SheetsUsedInners * 0.01; } }

        //Second row calcs
        public double? PaperCost { get { return (InnerSheetPrice * SheetsUsedInners) + (CoverSheetPrice * CopyQuantity); } }
        public double? Buffer { get { return SmallJob || Urgent ? 1.5 : 1.1; } }
        public double? Finishings { get { return CutsCost + HolePunchCost + FoldingCost; } }
        public double? Multiplier
        {
            get
            {
                return SheetsUsedInners switch
                {
                    <= 20 => 3,
                    <= 40 => 2.5,
                    <= 160 => 2.25,
                    <= 320 => 2,
                    <= 640 => 1.75,
                    <= 1280 => 1.5,
                    _ => 2.25
                };
            }
        }
        public double? FileHandlingCost { get { return FileHandlingFee; } }

        //Third row calcs
        public double? JobCost
        {
            get
            {
                double? jobCost = (((PaperCost * Buffer) + Finishings) * Multiplier) + FileHandlingCost;
                if (jobCost < 15)
                {
                    jobCost = 15;
                }
                return jobCost;
            }
        }
        public double? JobCostWithGST { get { return JobCost * 1.15; } }


        public void Calculate(PaperCalc.Data.PaperCalcContext _context, String path)
        {
            Settings = new();
            Settings.SetSettings(path);
        }
    }
}
