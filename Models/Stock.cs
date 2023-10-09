using PaperCalc.Data;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaperCalc.Models
{

    //----- Paper Stock Classes -----//
    public abstract class Stock
    {
        [Key]
        public Guid Id { get; set; }
        public string Supplier { get; set; } = default!;
        [Display(Name = "Stock Brand")]
        public string StockBrand { get; set; } = default!;
        public int Weight { get; set; } = default!;
        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }
        [Display(Name = "Coating Type")]
        public CoatType CoatType { get; set; }
    }
    public class Sra3AndBookletsStock : Stock
    {
        public string Size { get; set; } = default!; //Possabily unneccesary
        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }
        [Display(Name = "Price/1000")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePer1000 { get; set; }
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePerPack { get { return Math.Round(PricePer1000 / (1000 / SheetsPerPack), 2); } }
        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PricePerPack / SheetsPerPack, 2); } }
        [Display(Name = "Height Of 100 Sheets")]
        public double HeightOf100Sheets { get; set; }
        public double HeightOfASheet { get { return HeightOf100Sheets / 100; } }
    }
    public class DocumentsStock : Stock
    {
        public string Size { get; set; } = default!;
        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }
        [Display(Name = "Price/1000")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePer1000 { get; set; }
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePerPack { get { return Math.Round(PricePer1000 / (1000 / SheetsPerPack), 2); } }
        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PricePerPack / SheetsPerPack, 2); } }
        [Display(Name = "Height Of 100 Sheets")]
        public double HeightOf100Sheets { get; set; }
        public double HeightOfASheet { get { return HeightOf100Sheets / 100; } }
    }
    public class EpsonStock : Stock
    {
    }

    //----- Other Items Stock Classes -----//


    public class BindingCoverStock
    {
        [Key]
        public Guid Id { get; set; }
        public string Supplier { get; set; } = default!;
        [Display(Name = "Stock Brand")]
        public string StockBrand { get; set; } = default!;
        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }
        public string Size { get; set; } = default!;
        [Display(Name = "Sheets Per Pack")]
        public double SheetsPerPack { get; set; }
        [Display(Name = "Pack Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double PricePerPack { get; set; }
        [Display(Name = "Sheet Cost")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double SheetCost { get { return Math.Round(PricePerPack / SheetsPerPack, 2); } }
    }
    public class BindingCoilsStock
    {
        [Key]
        public Guid Id { get; set; }
        public BindingCoilType BindingCoilType { get; set; }
        public string RingsRatio { get; set; } = default!;
        public int SheetsHeld { get; set; }
        public double CoilSize { get; set; }
        public double CoilsPerPack { get; set; }
        public double PricePerPack { get; set; }
        public double PricePerCoil { get { return Math.Round(PricePerPack / CoilsPerPack, 2); } }
        public string? Description { get { return $"{CoilSize}mm  {BindingCoilType} bound"; } }
    }

    //----- Enums for Stock -----//

    public enum CoatType
    {
        Coated,
        Uncoated,
        Matt,
        Satin,
        Gloss,
    }
    public enum StockType
    {
        Adhesive,
        Synthetic,
        Paper,
        Card,
        CardTextured,
        CardRecycled,
        Acetate,
        Leather,
    }
    public enum BindingCoilType
    {
        None,
        Plastic,
        Wire,
    }
}
