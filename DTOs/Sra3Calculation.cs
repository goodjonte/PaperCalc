using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using Microsoft.DotNet.Scaffolding.Shared;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    [NotMapped]
    public class Sra3Calculation
    {
        //Constructors
        public Sra3Calculation(PaperCalc.Data.PaperCalcContext _context, string path, Sra3FormInput inputs)
        {
            Sra3AndBookletsStock? stock = _context.Sra3AndBookletsStock.Find(inputs.StockId);
            FlatSize? flatsize = _context.FlatSizes.Find(inputs.FlatSizeId);
            Sra3Stock = stock ?? new();
            FlatSize = flatsize ?? new() { Name = " " };
            Context = _context;
            Inputs = inputs;
            Settings = new();
            Settings.SetSettings(path);

            if (!Inputs.CustomFlatSize)
            {
                Inputs.Height = FlatSize.Height;
                Inputs.Width = FlatSize.Width;
            }

            CuttingCalculation = new(Inputs.Quantity, Sra3Stock.HeightOfASheet, Inputs.Height, Inputs.Width);
        }
        //Classes Required For Calculation
        public PaperCalcContext? Context { get; set; }
        public Settings Settings { get; set; }
        public FlatSize? FlatSize { get; set; }
        public Sra3AndBookletsStock Sra3Stock { get; set; }
        public Sra3FormInput Inputs { get; set; }
        public CuttingCalculation CuttingCalculation { get; set; }

        //Calculations
        public double ClickRate
        {
            get
            {
                double clickrate = Settings.ClickRateBase;
                clickrate = Inputs.Colour ? clickrate * 2 : clickrate;
                clickrate = Inputs.DoubleSided ? clickrate * 2 : clickrate;
                return clickrate;
            }
        }
        public double CreasingCost
        {
            get
            {
                if(Inputs.Creases > 0)
                {
                    return Inputs.Creases * Settings.CreasingBase * Inputs.Quantity;
                }
                return 0;
            }
        }
        public double FoldingCost
        {
            get
            {
                if (Inputs.Folds > 0)
                {
                    return Inputs.Folds * Settings.FoldingBase * Inputs.Quantity;
                }
                return 0;
            }
        }
        public double HolePunchesCost
        {
            get
            {
                if (Inputs.HolePunches > 0)
                {
                    return Inputs.HolePunches * Inputs.Quantity / Settings.HolePunchingBase;
                }
                return 0;
            }
        }
        public double StaplesCost
        {
            get
            {
                if (Inputs.Staples > 0)
                {
                    return Inputs.Quantity * Inputs.Staples * Settings.StaplingBase;
                }
                return 0;
            }
        }

        //Totals
        public double PaperCost { get { return CuttingCalculation.SheetsUsed * Sra3Stock.SheetCost; } }
        public double ClickCost { get { return CuttingCalculation.SheetsUsed * ClickRate; } }
        public double MaterialCost { get { return PaperCost + ClickCost; } }
        public double FinishingCost {
            get
            {
                if (Inputs.Quantity >= CuttingCalculation.PerSra3)
                {
                    return (CuttingCalculation.CutsRequired / (100 / 60)) + ((CreasingCost + FoldingCost) / (200 / 60)) + (HolePunchesCost * (50 / 60) + StaplesCost);
                }
                else
                {
                    return (CuttingCalculation.CutsRequired / (60 / 60)) + ((CreasingCost + FoldingCost) / (200 / 60)) + (HolePunchesCost * (50 / 60) + StaplesCost);
                }
            }
        }
        //Factors
        public double Buffer { get { return CuttingCalculation.SheetsUsed < 10 ? Settings.BufferHigh : Settings.Buffer; } }
        public double Multiplier {
            get
            {
                if (Inputs.CustomFlatSize && Context != null) //Havent Test yet
                {
                    double area = Inputs.Height * Inputs.Width;
                    var allFlats = Context.FlatSizes.ToList();
                    FlatSize? roundedUptoFlat = null;
                    for(int i = 0; i < allFlats.Count; i++)
                    {
                        if (allFlats[i].ForCalculation == CalculationType.Sra3)
                        {
                            if(roundedUptoFlat == null && allFlats[i].Area > area)
                            {
                                roundedUptoFlat = allFlats[i];
                                continue;
                            }
                            if (allFlats[i].Area > area && roundedUptoFlat != null && allFlats[i].Area < roundedUptoFlat.Area)
                            {
                                roundedUptoFlat = allFlats[i];
                            }
                        }
                    }
                    if(roundedUptoFlat != null)
                    {
                        return roundedUptoFlat.CalculateMultiplier(Inputs.Quantity);
                    }
                }

                return FlatSize.CalculateMultiplier(Inputs.Quantity);
            }
        } //TODO - can use my method or his
        //Charges
        public double MaterialCharge { get { return MaterialCost * Buffer * Multiplier; } }
        public double LabourCharge {
            get
            {
                return CuttingCalculation.CuttingLabour + CreasingCost + FoldingCost + HolePunchesCost + StaplesCost; 
            }
        }

        //Totals
        public double TotalCost { get { return MaterialCost + FinishingCost; } }
        public double FinalCharge { get { return Math.Ceiling(MaterialCharge + LabourCharge); } }

        //Profit
        public double MaterialProfit { get { return MaterialCharge - MaterialCost; } }
        public double LabourProfit { get { return LabourCharge - FinishingCost; } }
        public double TotalProfit { get { return LabourProfit + MaterialProfit; } }

        //Final Calculations For FrontEnd
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCost {
            get
            {
                if (Inputs.Quantity < 1) return 0;
                if (FlatSize == null) return 0;
                if (Inputs.Quantity > FlatSize.QuantityMax) //This is a shortcut fix for now ideally we want to make temp calc this calc possibly
                {
                    var tempJob = Inputs;
                    tempJob.Quantity = FlatSize.QuantityMax;
                    var tempCalc = new Sra3Calculation(Context, Settings.PathForSettings, tempJob);
                    return Inputs.Quantity * tempCalc.CostPerunit;
                }
                double jobCost = FinalCharge + Inputs.FileHandlingCost + Inputs.DesignCost + Inputs.SetupCost;// add 4 if for packaging cost, we can make this a setting
                return jobCost < Settings.MinimumJobCost ? Settings.MinimumJobCost : jobCost;
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCost { get { return Inputs.Kinds == 1 ? JobCost : JobCost * Inputs.Kinds * Settings.KindsMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCostWithGst { get { return FinalJobCost * Settings.Gst; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double CostPerunit
        {
            get
            {
                return JobCost / Inputs.Quantity;
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double GST { get { return FinalJobCostWithGst - FinalJobCost; } }

        public string? Description
        {
            get
            {
                string size = FlatSize != null ? FlatSize.Name : "Custom";
                string stock = $"{Sra3Stock.CoatType} {Sra3Stock.StockType} - {Sra3Stock.Weight}gsm";
                string sides = Inputs.DoubleSided ? "D/S" : "S/S";
                string colour = Inputs.Colour ? "Colour" : "B&W";
                string crease = Inputs.Creases > 0 ? $"{Inputs.Creases} x Crease, " : "";
                string fold = Inputs.Folds > 0 ? $"{Inputs.Folds} x Folds, " : "";
                string holePunches = Inputs.HolePunches > 0 ? $"{Inputs.HolePunches} x Drill, " : "";
                string staple = Inputs.Staples > 0 ? $"{Inputs.Staples} x Staple, " : "";
                string lamination = Inputs.Lamination ? ", Laminated" : "";
                string kinds = Inputs.Kinds > 1 ? $" {Inputs.Kinds} kinds" : "";

                return $"@{Inputs.Quantity}qty{kinds} - {size} size, {crease}{fold}{holePunches}{staple}{stock}, {sides}, {colour}{lamination}";
            }
        }
    }
}
