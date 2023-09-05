namespace PaperCalc.DTOs
{
    public class WideFormatCalculation
    {
        public Settings? Settings { get; set; }
        public int? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        //public WideFormatFlatSize? FlatSize { get; set; }
        public string? Colour { get; set; }
        public bool Trimming { get; set; }
        public bool Lamination { get; set; }
        public int Creasing { get; set; }
        public int Folds { get; set; }
        public bool SmallJob { get; set; }
        public bool Urgent { get; set; }
        public bool FileHandling { get; set; }
        public double? FileHandlingFee { get; set; }

        //First Row Calcs
        public double? SheetCostPerMeter { get; }
        public double? StockCost { get; }
        public double? InkCostPerMeter { get; }
        public string? RollLength { get; }
        public double? CutsCost { get; }
        public double? LaminationCost { get; }
        public double? LaminationCutsCost { get; }
        public double? CreasingCost { get; }
        public double? FoldingCost { get; }

        //Second Row Calcs
        public double? PaperCost { get; }
        public double? Buffer { get; }
        public double? Finishings { get; }
        public string? Multiplier { get; }
        public double? TrimmingCost { get; }
        public double? FileHandlingCost { get; }
        public double? ColourMultiplier { get; }

        //Third Row Calcs
        public double? JobCost { get; }
        public double? JobCostWithGST { get { return JobCost * 1.15; } }
    }
}
