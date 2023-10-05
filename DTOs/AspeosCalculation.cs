using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.DotNet.Scaffolding.Shared;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class AspeosCalculation
    {
        public PaperCalcContext? Context { get; set; }
        public Settings? Settings { get; set; }

        public double? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public FlatSize? FlatSize { get; set; }
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

        //First Row Calculations
        public int PerSRA { get { return Height > 0 && Width > 0 ? SizeCalculation.CalculatePerSra3(Height, Width) : 0; } }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double BaseClickRate { get { return Colour == null ? 0 : Colour == "colour" ? 0.08 : 0.01; } } //hardcoded in click rate
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSideMultiplier { get { return PrintedSides == null ? 0 : PrintedSides == "single" ? BaseClickRate : BaseClickRate * 2; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSizeMultiplier { get { return Trimming ? ClickSideMultiplier * 2 : ClickSideMultiplier; } } //This need a proper solution based on size - wtf is this even calculating come on bruh
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickRate { get { return ClickSizeMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetPrice { get; set; }
        public double Cuts { get { return Height > 0 && Width > 0 ? SizeCalculation.CalculateCuts(Height, Width) : 0; } }
        public double? HolePunches { get { return NumOfHolePunches > 0 ? (Quantity / 20) * NumOfHolePunches : 0; } } //hardcoded??
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? LaminationCost { get {
                if (Lamination && Context != null)
                {
                    return LaminationStock.GetLaminationCost(Context, Width, Height) * Quantity;
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
                return Creasing switch
                {
                    0 => (double?)0,
                    1 => Quantity * Settings.Creasing1,
                    2 => Quantity * Settings.Creasing2,
                    3 => Quantity * Settings.Creasing3,
                    _ => (double?)0,
                };
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
                return Folds switch
                {
                    0 => (double?)0,
                    1 => Quantity * Settings.Folding1,
                    2 => Quantity * Settings.Folding2,
                    3 => Quantity * Settings.Folding3,
                    _ => (double?)0,
                };
            }
        }

        //Second Row Calculations

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PaperCost { get { return Quantity != null ? ((SheetPrice + ClickRate) / PerSRA) * Quantity : 0; } }

        public double Buffer { get { return Settings != null ? SmallJob || Urgent ? Settings.BufferSmallOrUrgent : Settings.Buffer : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? FinishingsCost { get { return (Cuts * 2) + HolePunches + LaminationCost + CreasingRate + FoldingRate; } }//why do we multiply cuts?? on sheets its actually in the imposistion calc

        public double? Multiplier { get { return CustomSize ? null : FlatSize != null ? FlatSize.CalculateMultiplier(Quantity) : null; } }

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
                if (Settings != null)
                {
                    if (jobCost < Settings.MinimumJobCost)
                    {
                        return Settings.MinimumJobCost;
                    }
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
            //Set Context and Settings
            Context = _context;
            Settings = new();
            Settings.SetSettings(path);

            //Return if custom size as height and width are already set
            if (CustomSize || FlatSizeId == null)
            {
                return;
            }

            //Set FlatSize, Height and Width
            FlatSize = _context.FlatSizes.Find(FlatSizeId);
            if(FlatSize == null) return;
            Width = FlatSize.Width;
            Height = FlatSize.Height;
        }

        
    }
}
