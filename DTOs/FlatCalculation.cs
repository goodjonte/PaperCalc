using PaperCalc.Models;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.DTOs
{
    public class FlatCalculation
    {
        public Settings? Settings { get; set; }
        public int? Pages { get; set; }
        public int? CopyQuantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public FlatFlatSize? FlatSize { get; set; }
        public string? Colour { get; set; }
        public string? PrintedSides { get; set; }
        public bool Binding { get; set; }
        public int NumOfHolePunches { get; set; }
        public bool Lamination { get; set; }
        public int Creasing { get; set; }
        public int Folds { get; set; }
        public bool Urgent { get; set; }
        public bool FileHandling { get; set; }
        public double? FileHandlingFee { get; set; }

        //first row calcs
        public int? SheetsUsed { get { return Pages * CopyQuantity; } }
        public double? BaseClickRate { get { return Colour == "black" ? 0.01 : 0.08; } } //Hard coded in click rates
        public double? ClickSideMultiplier { get { return PrintedSides == "single" ? BaseClickRate * 1 : BaseClickRate * 2; } }
        public double? ClickSizeMultiplier { get { return FlatSize != null ? ClickSideMultiplier * FlatSize.SizeMultiplier : 0 ; } }
        public double? ClickRate { get { return ClickSizeMultiplier; } }
        public double? SheetPrice { get; set; } //set in front end
        public double? BindingCost { get { return Binding ? CopyQuantity * 7.7 : 0; } } //Hard coded in binding multiplier
        public double? HolePunchesCost { get { return Pages / 20 * NumOfHolePunches; } }
        public double? LaminationCost { get { return Lamination && FlatSize != null ? FlatSize.LaminationCost * SheetsUsed: 0; } } //dont think this is right -broken on sheets? - i made this multiply by sheets used instead of whats on sheets which is LamCost * Pages * CopyQuantity
        public double? CreasingCost {// Hard coded in creasing multiplier
            get
            {
                return Creasing switch
                {
                    1 => 0.1 * SheetsUsed,
                    2 => 0.15 * SheetsUsed,
                    3 => 0.2 * SheetsUsed,
                    _ => (double?)0,
                };
            }
        }
        public double? FoldingCost {
            get
            {
                return Folds switch
                {
                    1 => 0.1 * SheetsUsed,
                    2 => 0.15 * SheetsUsed,
                    3 => 0.2 * SheetsUsed,
                    _ => (double?)0,
                };
            }
        }
        public int? LamCuts { get { return Lamination ? 8 * Pages : 0; } }

        //second row calcs
        public double? PaperCost {
            get
            {
                if(PrintedSides == "double")
                {
                    return (SheetPrice + ClickRate) * (Pages / 2 * CopyQuantity);
                }
                else if(PrintedSides == "single")
                {
                    return (SheetPrice + ClickRate) * (Pages * CopyQuantity);
                }
                else
                {
                    return 0;
                }
            }
        }
        public double? Buffer { get { return Urgent ? 1.5 : 1.1; } } //hardcoded in buffer
        public double? Finishings { get { return BindingCost + HolePunchesCost + LaminationCost + FoldingCost; } }
        public double? Multiplier{ //hard coded in multipliers
            get
            {
                if(FlatSize != null)
                {
                    if (FlatSize.Name == "A4")
                    {
                        return SheetsUsed switch
                        {
                            < 10 => (double?)3,
                            < 50 => 2.75,
                            < 100 => 2.5,
                            < 250 => (double?)2,
                            < 500 => 1.75,
                            < 1000 => 1.5,
                            _ => (double?)2,
                        };
                    }
                    else
                    {
                        return SheetsUsed switch
                        {
                            < 5 => (double?)3,
                            < 10 => 2.75,
                            < 50 => 2.5,
                            < 100 => (double?)2,
                            < 250 => 1.75,
                            < 500 => 1.5,
                            _ => (double?)2,
                        };
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
        public double? Minimum { get { return Urgent ? 15 : 0; } } //hardcoded in minimum for urgent cost
        public double? FileHandlingCost { get { return FileHandlingFee != null ? FileHandlingFee : 0; } } //Set in front end

        //third row calcs
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCost {
            get
            {
                if((((PaperCost * Buffer) + Finishings) * Multiplier) + Minimum + FileHandlingCost + LamCuts < 15)
                {
                    return 15;
                }
                else
                {
                    return (((PaperCost * Buffer) + Finishings) * Multiplier) + Minimum + FileHandlingCost + LamCuts;
                }
            }
        }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCostWithGST { get { return JobCost * 1.15; } }
        //there was an unlabeled cell on sheets inb third row

        //Methods

        public void Calculate(PaperCalc.Data.PaperCalcContext _context, String path)
        {
            Settings = new();
            Settings.SetSettings(path);
            FlatSize = _context.FlatFlatSizes.Find(FlatSizeId);
        }
    }
}
