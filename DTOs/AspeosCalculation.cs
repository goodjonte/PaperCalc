using System.ComponentModel.DataAnnotations;
using System.Drawing;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class AspeosCalculation
    {
        public PaperCalcContext? Context { get; set; }
        public Settings? Settings { get; set; }
        public SizeCalculation? CustomFlatSize { get; set; } //Half done - am calculating persra3 and cuts for custom sizes but havent sorted lamination
        public double? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public CoatType? CoatType { get; set; }
        public bool CustomSize { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public AspeosFlatSize? FlatSize { get; set; }
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

        //First Row Calculations
        public int PerSRA { get { return CustomSize && CustomFlatSize != null ? CustomFlatSize.PerSra3CustomSize : (FlatSize?.PiecesPerSRA3) ?? 0; } }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double BaseClickRate { get { return Colour == null ? 0 : Colour == "colour" ? 0.08 : 0.01; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSideMultiplier { get { return PrintedSides == null ? 0 : PrintedSides == "single" ? BaseClickRate : BaseClickRate * 2; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSizeMultiplier { get { return Trimming ? ClickSideMultiplier * 2 : ClickSideMultiplier; } } //This need a proper solution based on size
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickRate { get { return ClickSizeMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetPrice { get; set; }
        public double Cuts { get { return CustomSize && CustomFlatSize != null ? CustomFlatSize.CutsCustomSize : (FlatSize?.CutsPerSRA3) ?? 0; } }
        public double? HolePunches { get { return NumOfHolePunches > 0 ? (Quantity / 20) * NumOfHolePunches : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? LaminationCost { get {
                if (Lamination)
                {
                    if (CustomSize && Context != null)
                    {
                        return LaminationStock.GetLaminationCost(Context, Width, Height) * Quantity;
                    }
                    return FlatSize != null ? FlatSize.LaminationCost * Quantity : 0 ; //need to get rid of this and do it by flatsize size like with custom above
                }
                return 0;
            }
        }
        public double? CreasingRate
        {
            get
            {
                if (Settings == null)
                {
                    return 0;
                }
                switch (Creasing)
                {
                    case 0:
                        return 0;
                    case 1:
                        return Quantity * Settings.Creasing1;
                    case 2:
                        return Quantity * Settings.Creasing2;
                    case 3:
                        return Quantity * Settings.Creasing3;
                    default:
                        return 0;
                }
            }
        }

        public double? FoldingRate
        {
            get
            {
                if (Settings == null)
                {
                    return 0;
                }
                switch (Folds)
                {
                    case 0:
                        return 0;
                    case 1:
                        return Quantity * Settings.Folding1;
                    case 2:
                        return Quantity * Settings.Folding2;
                    case 3:
                        return Quantity * Settings.Folding3;
                    default:
                        return 0;
                }
            }
        }

        //Second Row Calculations

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PaperCost { get { return Quantity != null ? ((SheetPrice + ClickRate) / PerSRA) * Quantity : 0; } }

        public double Buffer { get { return Settings != null ? SmallJob || Urgent ? Settings.BufferSmallOrUrgent : Settings.Buffer : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? FinishingsCost { get { return (Cuts * 2) + HolePunches + LaminationCost + CreasingRate + FoldingRate; } }

        public double? Multiplier { get { return Settings != null ? SmallJob || Urgent ? Settings.MarginMultiplierSmallOrUrgent : Settings.MarginMultiplier : 0; } }

        public double? Minimum { get { return Settings != null ? SmallJob && Urgent ? Settings.SmallOrUrgentMinimum : 0 : 0; } }

        public double? FileHandlingCost
        {
            get
            {

                return FileHandlingFee != null ? FileHandlingFee : 0;

            }
        }

        //Third Row Calculations
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCost
        {
            get
            {
                if (Quantity == null) { return 0; }
                double? jobCost = (((PaperCost * Buffer) + FinishingsCost) * Multiplier) + Minimum + FileHandlingCost;
                if (jobCost < 15 && Settings != null)
                {
                    return Settings.MinimumJobCost;
                }
                return jobCost;
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCostGstInc { get { return JobCost > 0 ? JobCost * 1.15 : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? GST { get { return JobCostGstInc > 0 ? JobCostGstInc - JobCost : 0; } }



        //Method has to be called to calculate the values of the DTO - sets settings and flat size
        public void Calculate(PaperCalc.Data.PaperCalcContext _context, String path)
        {
            Context = _context;
            Settings = new();
            Settings.SetSettings(path);
            if (CustomSize)
            {
                CustomFlatSize = new();
                CustomFlatSize.Calculate(Height, Width);
                return;
            }
            FlatSize = _context.AspeosFlatSizes.Find(FlatSizeId);
        }

    }
}
