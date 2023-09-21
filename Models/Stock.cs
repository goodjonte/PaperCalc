using PaperCalc.Data;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }
        public string Company { get; set; } = default!;
        [Display(Name = "Product")]
        public string ProductName { get; set; } = default!;
        public int Weight { get; set; } = default!;
        [Display(Name = "Coating")]
        public CoatType CoatType { get; set; }
    }
    public enum CoatType
    {
        Adhesive,
        Coated,
        Uncoated,
        Synthetic,
        Matt,
        Satin,
        Gloss
    }

    public class AspeosStock : Stock
    {
        public string Size { get; set; } = default!;
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCost { get; set; }

        [Display(Name = "Pack Cost + GST")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCostGstIncluded { get { return Math.Round(PackCost * 1.15, 2); } }

        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }

        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PackCost / SheetsPerPack, 2); } }
    }

    public class EpsonStock : Stock
    {
        public int Width { get; set; }
        [Display(Name = "Roll Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double RollCost { get; set; }

        [Display(Name = "Roll Cost (GST Incl)")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double RollCostGstIncluded { get { return Math.Round(RollCost * 1.15, 2); } }

        [Display(Name = "Rolls Length (meters)")]
        public double RollsLength { get; set; }

        [Display(Name = "Price Per Meter")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePerMeter { get { return Math.Round(RollCost / RollsLength, 2); } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA0 { get { return Width >= 841 ? (PricePerMeter / 1000) * 1189 : null; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA1 { get { return PriceA0 != null ? (PricePerMeter / 1000) * 594 : Width >= 594 ? (PricePerMeter / 1000) * 841 : null; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA2 { get { return Width >= 420 ? (PricePerMeter / 1000) * 420 : null; } }
    }

    public class FlatStock : Stock
    {
        public string Size { get; set; } = default!;
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCost { get; set; }

        [Display(Name = "Pack Cost + GST")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PackCostGstIncluded { get { return Math.Round(PackCost * 1.15, 2); } }

        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }

        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PackCost / SheetsPerPack, 2); } }
    }

    public class LaminationStock : Stock
    {
        public int Width { get; set; }
        [Display(Name = "Roll Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double RollCost { get; set; }

        [Display(Name = "Roll Cost (GST Incl)")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double RollCostGstIncluded { get { return Math.Round(RollCost * 1.15, 2); } }

        [Display(Name = "Rolls Length (meters)")]
        public double RollsLength { get; set; }

        [Display(Name = "Price Per Meter")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePerMeter { get { return Math.Round(RollCost / RollsLength, 2); } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA0 { get { return Width >= 841 ? (PricePerMeter / 1000) * 1189 : null; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA1 { get { return PriceA0 != null ? (PricePerMeter / 1000) * 594 : Width >= 594 ? (PricePerMeter / 1000) * 841 : null; } }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double? PriceA2 { get { return Width >= 420 ? (PricePerMeter / 1000) * 420 : null; } }

        public static double GetLaminationCost(PaperCalcContext context, double width, double length)
        {
            double lamUsed = width > length ? (width + 10) * 2 : (length + 10) * 2;
            LaminationStock? lamStock = context.LaminationStock.FirstOrDefault();
            if(lamStock != null)
            {
                return Math.Round(((lamStock.PricePerMeter * 2)/1000) * lamUsed, 2);
            }
            return 0;
        }
    }
}
