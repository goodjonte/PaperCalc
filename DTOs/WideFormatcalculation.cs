using PaperCalc.Data;
using PaperCalc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace PaperCalc.DTOs
{
    [NotMapped]
    public class WideFormatCalculation
    {
        //Constructor
        public WideFormatCalculation(PaperCalc.Data.PaperCalcContext _context, string path, WideFormatFormInputs inputs)
        {
            Context = _context;
            Inputs = inputs;
            Settings = new();
            Settings.SetSettings(path);

            FlatSize? flatSize = _context.FlatSizes.Find(inputs.FlatSizeId);
            WideFormatStock? stock = _context.WideFormatStock.Find(inputs.StockId);

            FlatSize = flatSize ?? new() { Name = "" };
            Paper = stock ?? new();

            Inputs.Height = FlatSize.Height;
            Inputs.Width = FlatSize.Width;

            Inputs.Kinds = 1; //setting to 1 as we are not using this feature for now
        }

        //Required Classes
        public PaperCalcContext Context { get; set; }
        public Settings Settings { get; set; }
        public FlatSize FlatSize { get; set; }
        public WideFormatFormInputs Inputs { get; set; }
        public WideFormatStock Paper { get; set; }

        //Calculations
        public double ShortestEdge { get { return FlatSize.Width < FlatSize.Height ? FlatSize.Width : FlatSize.Height; } }
        public double LongestEdge { get { return FlatSize.Width < FlatSize.Height ? FlatSize.Height : FlatSize.Width; } }
        public bool LongRoll { get { return ShortestEdge > 594; } }
        public double MetresUsed
        {
            get
            {
                if (LongRoll)
                {
                    return ShortestEdge * Inputs.Quantity * Inputs.Pages / 1000;
                }
                else
                {
                    return LongestEdge * Inputs.Quantity * Inputs.Pages / 1000;
                }
            }
        }
        public double CutsNeeded { get { return Paper.Weight == 80 && Paper.CoatType == CoatType.Uncoated ? 0 : Inputs.Quantity * Inputs.Pages * 4; } } // On sheets one stock item requires no cuts - now sorted but not perfect
        public double CreasesNeeded { get { return Inputs.Creases > 0 ? Inputs.Quantity * Inputs.Pages * Inputs.Creases : 0; } }
        public double FoldsNeeded { get { return Inputs.Folds > 0 ? Inputs.Quantity * Inputs.Pages * Inputs.Folds : 0; } }


        //Cost Totals
        public double PaperCost { get { return Paper.PricePerMeter * MetresUsed; } }
        public double InkCost { get { return Inputs.Colour ? 2.24 * MetresUsed : 2.24 * MetresUsed * 0.85; } } // HardCoded Ink Coverage Per Meter (2.24) - Add to Settings
        public double MaterialCost { get { return PaperCost + InkCost; } }
        public double FinishingCost { get { return (CreasesNeeded * 0.15) + (CutsNeeded * 0.15) + (FoldsNeeded * 0.15); } } // HardCoded creasing,custs,folds cost - Add to Settings

        //Factors
        public double Multiplier { get { return FlatSize.CalculateMultiplier(Inputs.Quantity * Inputs.Pages); } }
        public double Buffer { get { return (Inputs.Quantity * Inputs.Pages) < 2 ? 2 : 1.5; } }


        //Charge Totals
        public double MaterialCharge { get { return MaterialCost * Buffer * Multiplier; } }
        public double FinishingCharge { get { return (CreasesNeeded * 0.25) + (CutsNeeded * 0.25) + (FoldsNeeded * 0.25); } } // HardCoded creasing,custs,folds charge - Add to Settings


        //Totals
        public double Cost { get { return MaterialCost + FinishingCost;  } }
        public double Charge { get { return Math.Ceiling(MaterialCharge + FinishingCharge); } }


        //Profit
        public double MaterialProfit { get { return MaterialCharge - MaterialCost; } }
        public double FinishingProfit { get { return FinishingCharge - FinishingCost; } }
        public double Profit { get { return MaterialProfit + FinishingProfit; } }


        //Front End Totals
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double JobCost
        {
            get
            {
                double jobCost = Charge + Inputs.FileHandlingCost + Inputs.DesignCost + Inputs.SetupCost;//4 if for packaging cost, we can make this a setting
                return jobCost < Settings.MinimumJobCost ? Settings.MinimumJobCost : jobCost;
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCost { get { return Inputs.Kinds < 2 ? JobCost : JobCost * Inputs.Kinds * 0.80; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double FinalJobCostWithGST { get { return JobCost * Settings.Gst; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double CostPerUnit { get { return FinalJobCostWithGST / Inputs.Quantity; } }

        public string? Description // TODO
        {
            get
            {
                var size = Inputs.CustomFlatSize ? $"Custom Size {Inputs.Height} x {Inputs.Width}" : $"{FlatSize.Name} {FlatSize.Height} x {FlatSize.Width}";
                var colour = Inputs.Colour ? "Colour" : "B/W";
                return $"{Inputs.Quantity} Copies @{Inputs.Pages}qty - {size}, {colour} on {Paper.CoatType} Paper - {Paper.Weight}gsm";
            }
        }
    }
}
