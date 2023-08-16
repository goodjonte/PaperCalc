using System.ComponentModel.DataAnnotations;
using System.Drawing;
using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    public class AspeosCalculation
    {
        public double? Quantity { get; set; }
        public Guid? FlatSizeId { get; set; }
        public AspeosFlatSize? FlatSize { get; set; }
        public Guid? CoatingId { get; set; }
        public string? Colour { get; set;}
        public string? PrintedSides { get; set;}
        public bool Trimming { get; set;}
        public bool HolePunch { get; set; }
        public bool Lamination { get; set; }
        public bool SmallJob { get; set; }
        public bool Urgent { get; set; }
        public bool FileHandling { get; set; }
        public int Creasing { get; set; }
        public int Folds { get; set; }

        //Some Value are hard coded in but could be moved to settings
        public int PerSRA { get { return (FlatSize?.PiecesPerSRA3) ?? 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double BaseClickRate { get { return Colour == null ? 0 : Colour == "colour" ? 0.08 : 0.01; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSideMultiplier { get { return PrintedSides == null ? 0 : PrintedSides == "single" ? BaseClickRate : BaseClickRate * 2; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickSizeMultiplier {  get { return Trimming ? ClickSideMultiplier * 2 : ClickSideMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double ClickRate {  get { return ClickSizeMultiplier; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetPrice { get; set; }
        public double Cuts { get { return (FlatSize?.CutsPerSRA3) ?? 0; } }
        public double? HolePunches { get { return HolePunch ? Quantity / 20 : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? LaminationCost { get { return FlatSize != null ?Lamination ? FlatSize.LaminationCost * Quantity : 0 : 0; } }
        public double? CreasingRate { get { return Creasing == 0 ? 0 : Creasing == 1 ? Quantity * 0.1 : Creasing == 2 ? Quantity * 0.15 : Creasing == 3 ? Quantity * 0.2 : 0 ; } }
        public double? FoldingRate { get { return Folds == 0 ? 0 : Folds == 1 ? Quantity * 0.1 : Folds == 2 ? Quantity * 0.15 : Folds == 3 ? Quantity * 0.2 : 0;} }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PaperCost { get { return Quantity != null ? ((SheetPrice + ClickRate) / PerSRA )* Quantity : 0; } }
        //Buffer values hard coded - change this
        public double Buffer { get { return SmallJob || Urgent ? 1.5 : 1.1; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? FinishingsCost { get { return (Cuts*2)+HolePunches+LaminationCost+CreasingRate+FoldingRate; } }
        //Multiplier values hard coded - change this
        public double? Multiplier { get { return SmallJob || Urgent ? 2 : 1.4; } }
        //Minimum value hard coded - change this
        public double? Minimum { get { return SmallJob && Urgent ? 15 : 0; } }
        //MiFileHandling value hard coded - change this
        public double? FileHandlingCost { get { return FileHandling ? 25 : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCost { get { return Quantity > 0 ? (((PaperCost * Buffer) + FinishingsCost) * Multiplier) + Minimum + FileHandlingCost : 0; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? JobCostGstInc { get { return JobCost > 0 ? JobCost*1.15 : 0; } }

        public void Calculate(PaperCalc.Data.PaperCalcContext _context)
        {
            AspeosStockCoatings? aspeosStockCoatings = _context.AspeosStockCoatings.Find(CoatingId);
            if (aspeosStockCoatings != null)
            {
                SheetPrice = aspeosStockCoatings.GetAverage(_context);
                FlatSize = _context.AspeosFlatSizes.Find(FlatSizeId);
            }
        }
    }
}
