﻿using PaperCalc.Data;
using PaperCalc.Models;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.DTOs
{
    public class BookletFormInputs
    {
        public double Quantity { get; set; }
        public double Pages { get; set; }
        public Guid? FlatSizeId { get; set; }
        public bool CustomFlatSize { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public Guid CoverStockId { get; set; }
        public Guid InnerStockId { get; set; }
        public bool Colour { get; set; }
        public int HolePunches { get; set; }
        public double FileHandlingCost { get; set; }
        public double DesignCost { get; set; }
        public double SetupCost { get; set; }
    }
    public class BookletCalculation
    {
        //Constructor
        public BookletCalculation(PaperCalc.Data.PaperCalcContext _context, String path, BookletFormInputs inputs)
        {
            Context = _context;
            Inputs = inputs;
            Settings = new();
            Settings.SetSettings(path);

            FlatSize? flatSize = Context.FlatSizes.Find(Inputs.FlatSizeId);
            Sra3AndBookletsStock? coverStock = Context.Sra3AndBookletsStock.Find(Inputs.CoverStockId);
            Sra3AndBookletsStock? innerStock = Context.Sra3AndBookletsStock.Find(Inputs.InnerStockId);

            FlatSize = flatSize ?? new() { Name = "" };
            CoverStock = coverStock ?? new();
            InnerStock = innerStock ?? new();

            Inputs.Height = FlatSize.Height;
            Inputs.Width = FlatSize.Width;
        }

        //Required Classes
        public PaperCalcContext Context { get; set; }
        public Settings Settings { get; set; }
        public FlatSize FlatSize { get; set; }
        public BookletFormInputs Inputs { get; set; }
        public Sra3AndBookletsStock CoverStock { get; set; }
        public Sra3AndBookletsStock InnerStock { get; set; }

        //Calculations
        public double ClickRate
        {
            get
            {
                double clickrate = 0.02; //HardCoded for now - need to add clickrate model etc
                clickrate = Inputs.Colour ? clickrate * 2 : clickrate;
                clickrate = clickrate * 2;//Double sided is always true for booklets so me multiply again
                return clickrate;
            }
        }
        public double PaperCostPerBooklet
        {
            get
            {
                return CoverStock.SheetCost + (InnerStock.SheetCost * (Inputs.Pages - 4) / 4);
            }
        }

        public double TotalPages { get { return Math.Ceiling(Inputs.Pages / 4) * 4; } }
        public double BookletInnersused { get { return (TotalPages - 4) / 4; } }
        public double TotalInners { get { return BookletInnersused * Inputs.Quantity; } }
        public double BookletThickness { get { return ((InnerStock.HeightOfASheet * BookletInnersused) + CoverStock.HeightOfASheet ) * 2; } }
        public double TotalThickness { get { return BookletThickness * Inputs.Quantity; } }
        public double BookletsPerBundle { get { return Math.Floor(20 / BookletThickness); } }
        public double Bundles { get { return Math.Ceiling(Inputs.Quantity / BookletsPerBundle); } }

        public double CutsRequired { get { return Bundles * 3; } }
        public double CutsCharge { get { return CutsRequired * 2; } }
        public double CreasingCost { get { return 0.1 * Inputs.Quantity < 1 ? 1 : 0.1 * Inputs.Quantity; } }
        public double FoldingCost { get { return 0.1 * Inputs.Quantity < 1 ? 1 : 0.1 * Inputs.Quantity; } }
        public double StaplesCost { get { return Inputs.Quantity * 2 * 0.05; } }
        public double HolePunchingCost {
            get
            {
                if (Inputs.HolePunches < 1) return 0;
                return (Inputs.HolePunches * Inputs.Quantity / 25) < 1 ? 1 : (Inputs.HolePunches * Inputs.Quantity / 25);
            }
        }

        //Cost Totals
        public double PaperCost { get { return (InnerStock.SheetCost * TotalInners) + (CoverStock.SheetCost * Inputs.Quantity); } }
        public double ClickCost { get { return ClickRate * (TotalInners + Inputs.Quantity); } }
        public double MaterialCost { get { return PaperCost + ClickCost + StaplesCost; } }
        public double LabourCost { get { return CutsRequired / (90 / 60); } }

        //Factors
        public double Buffer { get { return (TotalInners + Inputs.Quantity) < 32 ? 2 : 1.5; } }
        public double Multiplier { get { return (TotalInners + Inputs.Quantity) < 32 ? 2 : 1.5; } } // Sort this tododod

        //Charge Totals
        public double MaterialCharge { get { return MaterialCost * Buffer * Multiplier; } }
        public double LabourCharge { get { return CutsCharge + CreasingCost + FoldingCost + HolePunchingCost + StaplesCost; } }

        //Totals
        public double TotalCost { get { return MaterialCost + LabourCost; } }
        public double TotalCharge { get { return MaterialCharge + LabourCharge; } }

        //Profit
        public double MaterialProfit { get { return MaterialCharge - MaterialCost; } }
        public double LabourProfit { get { return LabourCharge - LabourCost; } }
        public double TotalProfit { get { return MaterialProfit + LabourProfit; } }

        //Front End Totals
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCost
        {
            get
            {
                double jobCost = TotalCharge + Inputs.FileHandlingCost + Inputs.DesignCost + Inputs.SetupCost;
                return jobCost < 15 ? 15 : jobCost;//Hardcoded Minimum Charge
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCostWithGST { get { return JobCost * 1.15; } }
        public string? Description // TODO
        {
            get
            {
                string size = FlatSize != null ? FlatSize.Name : "Custom";
                string stockCover = $"{CoverStock.CoatType} {CoverStock.StockType} - {CoverStock.Weight}gsm";
                string stockInner = $"{InnerStock.CoatType} {InnerStock.StockType} - {InnerStock.Weight}gsm";
                string colour = Inputs.Colour ? ", Colour" : ", B&W";
                string holePunches = Inputs.HolePunches > 0 ? $"{Inputs.HolePunches} x Drill, " : "";
                string pages = $"{Math.Ceiling(Inputs.Pages / 4) * 4}-page Saddlestitch Booklet ";

                return $"@{Inputs.Quantity}qty - {size} size, {holePunches}{pages}{colour} Covers: {stockCover}, Inners: {stockInner}";
            }
        }
    }

}
