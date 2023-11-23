using Elfie.Serialization;
using PaperCalc.Data;
using PaperCalc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Need to find out if we can just make it that the user just selects which bing coil to use - will need js to verify it fits coil size

namespace PaperCalc.DTOs
{
    [NotMapped]
    public class DocumentCalculation
    {
        //Constructor
        public DocumentCalculation(PaperCalc.Data.PaperCalcContext _context, string path, DocumentFormInputs inputs)
        {
            Context = _context;
            Inputs = inputs;
            Settings = new();
            Settings.SetSettings(path);

            FlatSize? flatSize = Context.FlatSizes.Find(Inputs.FlatSizeId);
            DocumentsStock? stock = Context.DocumentsStock.Find(Inputs.StockId);

            FlatSize = flatSize ?? new() { Name = "" };
            Stock = stock ?? new();

            Inputs.Height = FlatSize.Height;
            Inputs.Width = FlatSize.Width;

            Inputs.Kinds = 1; //setting to 1 as we are not using this feature for now
        }

        //Classes Required For Calculation
        public PaperCalcContext Context { get; set; }
        public Settings Settings { get; set; }
        public FlatSize FlatSize { get; set; }
        public DocumentFormInputs Inputs { get; set; }
        public DocumentsStock Stock { get; set; }

        //Calculations
        public double ClickRate
        {
            get
            {
                double clickrate = Inputs.Colour ? Settings.A4ColourClick : Settings.A4BlackClick;
                clickrate = Inputs.DoubleSided ? clickrate * 2 : clickrate;
                return clickrate;
            }
        }
        public double OutPutPages
        {
            get
            {
                //calculation is Inputs.DoubleSided ? Inputs.Pages / 2 : Inputs.Pages - But we need to round up to a even number
                return Inputs.DoubleSided ? Math.Round(Inputs.Pages / 2, MidpointRounding.AwayFromZero) : Inputs.Pages;
            }
        }
        public double SheetsUsed
        {
            get
            {
                return Inputs.Quantity * OutPutPages;
            }
        }
        public double DocumentSpine {
            get
            {
                return Stock.HeightOfASheet * (SheetsUsed / Inputs.Quantity);
            }
        }
        public double TotalHeight
        {
            get
            {
                return Stock.HeightOfASheet * SheetsUsed;
            }
        }
        public double BindingCoversCost//This might not work - could come to issues if stock changes in future - maybe could assign covers to paper stock?
        {
            get
            {
                if(Inputs.Binding == BindingCoilType.None)
                {
                    return 0;
                }
                var bindingCovers = Context.BindingCoverStock.Where(bc => bc.Size == FlatSize.Name);
                double cost = 0;
                foreach (var cover in bindingCovers)
                {
                    cost += cover.SheetCost ;
                }
                return cost * Inputs.Quantity;

            }
        }

        public BindingCoilsStock? BindingCoil
        {
            get
            {
                if (Inputs.Binding == BindingCoilType.None)
                {
                    return null;
                }
                var validType = Context.BindingCoilsStock.Where(bc => bc.BindingCoilType == Inputs.Binding);
                BindingCoilsStock? HigestestLessThanBc = null;
                foreach(var bc in validType)
                {
                    if(HigestestLessThanBc == null)
                    {
                        HigestestLessThanBc = bc;
                    }
                    else if(bc.SheetsHeld > SheetsUsed && bc.SheetsHeld < HigestestLessThanBc.SheetsHeld)
                    {
                        HigestestLessThanBc = bc;
                    }
                }
                return HigestestLessThanBc;
            }
        }
        public double BindingCoilsCost {
            get
            {
                if (Inputs.Binding == BindingCoilType.None)
                {
                    return 0;
                }
                return BindingCoil!.PricePerCoil * Inputs.Quantity;
            }
        }
        public double BindingPunches
        {
            get
            {
                if (Inputs.Binding == BindingCoilType.None)
                {
                    return 0;
                }

                return Math.Ceiling((SheetsUsed + (Inputs.Quantity * 2)) / 15);

            }
        }
        public double BindingLabourCharge
        {
            get
            {
                return Inputs.Binding == BindingCoilType.None ? 0 : BindingPunches * Settings.BindingPunchChargePA;
            }
        }
        public double BindingLabourCost
        {
            get
            {
                return Inputs.Binding == BindingCoilType.None ? 0 : BindingPunches * Settings.BindingPunchCostPA;
            }
        }
        public double FoldingCost
        {
            get
            {
                if (Inputs.Folds > 0)
                {
                    return Inputs.Folds * 0.05 * SheetsUsed;
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
                    return Inputs.HolePunches * SheetsUsed / 25;
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
                    return Math.Ceiling(Inputs.Quantity * Inputs.Staples * 0.05);
                }
                return 0;
            }
        }

        //Cost Totals
        public double PaperCost { get { return SheetsUsed * Stock.SheetCost; } }
        public double ClickCost { get { return SheetsUsed * ClickRate; } }
        public double MaterialCost { get { return StaplesCost + BindingCoilsCost + BindingPunches + BindingCoversCost; } }
        public double FinishingCost {
            get
            {
                if(Double.IsNaN((FoldingCost / (250 / 60)) +(HolePunchesCost / (50 / 60)) + BindingLabourCost + StaplesCost)) { return 0; }
                return (FoldingCost / (250/60)) + (HolePunchesCost / (50 / 60)) + BindingLabourCost + StaplesCost;
            }
        }

        //Factors
        public double Buffer { get { return SheetsUsed < Settings.DocumentsBufferDecider ? Settings.DocumentsBufferHigh : Settings.DocumentsBuffer; } }
        public double Multiplier {
            get
            {
                return FlatSize.CalculateMultiplier(Inputs.Quantity);
            }
        }

        //Charge Totals
        public double MaterialCharge { get { return (PaperCost + ClickCost + MaterialCost) * (Buffer * Multiplier); } }
        public double LabourCharge { get { return FoldingCost + HolePunchesCost + StaplesCost + BindingLabourCharge; } }

        //Totals
        public double TotalCost { get { return MaterialCost + FinishingCost; } }
        public double FinalCharge { get { return Math.Ceiling(MaterialCharge + LabourCharge); } }

        //Profit Figures
        public double MaterialProfit { get { return MaterialCharge - PaperCost; } }
        public double LabourProfit { get { return LabourCharge - FinishingCost; } }
        public double TotalProfit { get { return MaterialProfit + LabourProfit; } }


        //Final Calculations For FrontEnd
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCost {
            get
            {
                double jobCost = FinalCharge + Inputs.FileHandlingCost + Inputs.DesignCost + Inputs.SetupCost;//4 if for packaging cost, we can make this a setting
                return jobCost < Settings.MinimumJobCost ? Settings.MinimumJobCost : jobCost;
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCost { get { return Inputs.Kinds == 1 ? JobCost : JobCost * Inputs.Kinds * Settings.KindsMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCostWithGST { get { return FinalJobCost * Settings.Gst; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double CostPerUnit { get { return FinalJobCostWithGST / Inputs.Quantity; } }
        public string? Description
        {
            get
            {
                string size = FlatSize != null ? FlatSize.Name : "Custom";
                string stock = $"{Stock.CoatType} {Stock.StockType} - {Stock.Weight}gsm";
                string sides = Inputs.DoubleSided ? ", D/S" : ", S/S";
                string colour = Inputs.Colour ? ", Colour" : ", B&W";
                string fold = Inputs.Folds > 0 ? $"{Inputs.Folds} x Folds, " : "";
                string holePunches = Inputs.HolePunches > 0 ? $"{Inputs.HolePunches} x Drill, " : "";
                string staple = Inputs.Staples > 0 ? $"{Inputs.Staples} x Staple, " : "";
                string lamination = Inputs.Lamination ? ", Laminated" : "";

                string bindingCoil = Inputs.Binding != BindingCoilType.None ? $"{BindingCoil.CoilSize}mm {BindingCoil.BindingCoilType} bound" : "";

                return $"@{Inputs.Quantity}qty - {fold}{holePunches}{staple}{bindingCoil} {size} document with {OutPutPages} pages {stock}{sides}{colour}{lamination}";
            }
        }
    }
}
