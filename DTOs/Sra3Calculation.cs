using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using Microsoft.DotNet.Scaffolding.Shared;
using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class Sra3FormInput
    {
        public double Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public bool CustomFlatSize { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public bool DoubleSided { get; set; }
        public bool Colour { get; set; }
        public bool Lamination { get; set; }
        public Guid StockId { get; set; }
        public int Creases { get; set; }
        public int Folds { get; set; }
        public int Staples { get; set; }
        public int HolePunches { get; set; }
        public double FileHandlingCost { get; set; }
        public double DesignCost { get; set; }
        public double SetupCost { get; set; }
    }
    public class Sra3Calculation
    {
        //Constructor
        public Sra3Calculation(PaperCalc.Data.PaperCalcContext _context, String path, Sra3FormInput inputs)
        {
            Sra3AndBookletsStock? stock = _context.Sra3AndBookletsStock.Find(inputs.StockId);
            FlatSize? flatsize = _context.FlatSizes.Find(inputs.FlatSizeId);
            Sra3Stock = stock ?? new();
            FlatSize = flatsize ?? new() { Name = " " };
            Context = _context;
            Inputs = inputs;
            Settings = new();
            Settings.SetSettings(path);

            Inputs.Height = FlatSize.Height;
            Inputs.Width = FlatSize.Width;

            CuttingCalculation = new(Inputs.Quantity, Sra3Stock.HeightOfASheet, Inputs.Height, Inputs.Width);
        }
        //Classes Required For Calculation
        public PaperCalcContext Context { get; set; }
        public Settings Settings { get; set; }
        public FlatSize FlatSize { get; set; }
        public Sra3AndBookletsStock Sra3Stock { get; set; }
        public Sra3FormInput Inputs { get; set; }
        public CuttingCalculation CuttingCalculation { get; set; }

        //Calculations
        public double ClickRate
        {
            get
            {
                double clickrate = 0.02; //HardCoded for now - need to add clickrate model etc - would be in settings
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
                    return Inputs.Creases * 0.1 * Inputs.Quantity; //hardcoded - we talked about making this cheaper each time
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
                    return Inputs.Folds * 0.05 * Inputs.Quantity; //hardcoded - we talked about making this cheaper each time
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
                    return Inputs.HolePunches * Inputs.Quantity / 25; //hardcoded - we talked about making this cheaper each time
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
                    return Inputs.Quantity * Inputs.Staples * 0.05; //hardcoded - we talked about making this cheaper each time
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
                    return (CuttingCalculation.CutsRequired / (100 / 60)) + ((CreasingCost + FoldingCost) / (500 / 60)) + (HolePunchesCost * (50 / 60) + StaplesCost);
                }
                else
                {
                    return (CuttingCalculation.CutsRequired / (60 / 60)) + ((CreasingCost + FoldingCost) / (250 / 60)) + (HolePunchesCost * (50 / 60)); //Missing stapling cost? - asked on sheets
                }
            }
        }
        public double Buffer { get { return CuttingCalculation.SheetsUsed < 10 ? 1.5 : 1.1; } }
        public double Multiplier { get { return 1.1; } } //TODO - can use my method or his

        public double MaterialCharge { get { return MaterialCost * Buffer * Multiplier; } }
        public double LabourCharge {
            get
            {
                return CuttingCalculation.CuttingLabour + CreasingCost + FoldingCost + HolePunchesCost + StaplesCost; 
            }
        }

        //Final BackEnd Calculations
        public double TotalCost { get { return MaterialCost + FinishingCost; } }
        public double FinalCharge { get { return Math.Ceiling(MaterialCharge + LabourCharge); } }

        //Profit BackEnd Calculations
        public double MaterialProfit { get { return MaterialCharge - MaterialCost; } }
        public double LabourProfit { get { return LabourCharge - FinishingCost; } }
        public double TotalProfit { get { return LabourProfit + MaterialProfit; } }

        //Final Calculations For FrontEnd
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCost {
            get
            {
                double jobCost = FinalCharge + Inputs.FileHandlingCost + Inputs.DesignCost + Inputs.SetupCost;
                return jobCost < 15 ? 15 : jobCost;//Hardcoded Minimum Charge
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCostWithGst { get { return JobCost * 1.15; } }

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

                return $"@{Inputs.Quantity}qty - {size} size, {crease}{fold}{holePunches}{staple}{stock}, {sides}, {colour}{lamination}";
            }
        }
    }
}
