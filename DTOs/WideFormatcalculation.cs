using PaperCalc.Data;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class WideFormatCalculation
    {
        //need to figure out how custom sizes would work for this 
        // things that depend on flatsize - Roll Lenght(Long/Short) - Stock Cost - Ink Coverage - Lamination
        // Roll Lenght(Long/Short) is set and most other calculations are based on that (long or short)
        // need to know if long / short is a simple fix or what? whats the logic behind it
        public PaperCalcContext? _context { get; set; }
        public Settings? Settings { get; set; }
        public int? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public FlatSize? FlatSize { get; set; }
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
        public double? SheetCostPerMeter { get; set; }//Set in front end
        public double? StockCost {
            get{
                if(FlatSize == null)  return null;

                switch (FlatSize.Name)
                {
                    case "A0":
                        return SheetCostPerMeter * 1.189;
                    case "A1":
                        if (RollLength == "long") return SheetCostPerMeter * 0.594;
                        if (RollLength == "short") return SheetCostPerMeter * 0.841;
                        return null;
                    case "A2":
                        return SheetCostPerMeter * 0.42;
                    default:
                        return null;
                }
            }
        } 
        public double? InkCostPerMeter { //Hard coded in the 1.3 - need to ask what it is and wehter it changes
            get
            {
                if (FlatSize == null) return null;

                switch (FlatSize.Name)
                {
                    case "A0":
                        return 1.3 * 1.189;
                    case "A1":
                        if (RollLength == "long") return 1.3 * 0.594;
                        if (RollLength == "short") return 1.3 * 0.841;
                        return null;
                    case "A2":
                        return 1.3 * 0.42;
                    default:
                        return null;
                }
            }
        }
        public string? RollLength { get { return FlatSize != null ? FlatSize.Name == "A0" ? "long" : "short" : null; } } //This actually need to be calculated some how based on size otherwise custom sizes wont work
        public double? CutsCost { get { return Trimming ? 8 * Quantity : 0; } }
        public double? LaminationCost {
            get
            {
                if(
                    Lamination == false ||
                    _context == null ||
                    FlatSize == null
                )return null;

                return LaminationStock.GetLaminationCost(_context, FlatSize.Width, FlatSize.Height) * Quantity;
                        
            }
        }
        public double? LaminationCutsCost { get { return Lamination ? 8 * Quantity : 0; } }
        public double? CreasingCost { //hard Coded
            get
            {
              switch(Creasing)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 0.1 * Quantity;
                    case 2:
                        return 0.15 * Quantity;
                    case 3:
                        return 0.2 * Quantity;
                    default:
                        return null;
                }
            }
        }
        public double? FoldingCost //Hard coded
        {
            get
            {
                switch (Folds)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 0.1 * Quantity;
                    case 2:
                        return 0.15 * Quantity;
                    case 3:
                        return 0.2 * Quantity;
                    default:
                        return null;
                }
            }
        }

        //Second Row Calcs
        public double? PaperCost { get { return (StockCost + InkCostPerMeter) * Quantity; } }
        public double? Buffer { get { return Quantity < 2 ? 2 : 1.5; } } //Hard coded
        public double? Finishings { get { return LaminationCost; } }
        public double? Multiplier { get { return FlatSize != null ? FlatSize.CalculateMultiplier(Quantity) : 0; } }
        public double? TrimmingCost { get { return CutsCost + LaminationCutsCost; } }
        public double? FileHandlingCost { get; set; } //set in front end
        public double? ColourMultiplier { get { return Colour == "colour" ? 1 : 0.75; } } //Hard coded

        //Third Row Calcs
        public double? JobCost { get { return (((PaperCost * Buffer) + InkCostPerMeter) * Multiplier * ColourMultiplier) + TrimmingCost + FileHandlingCost; } }
        public double? JobCostWithGST { get { return JobCost * 1.15; } }
    }
}
