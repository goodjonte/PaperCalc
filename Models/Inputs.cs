namespace PaperCalc.Models
{
    public class Inputs
    {
        public Guid Id { get; set; }
        public double Quantity { get; set; }
        public double FileHandlingCost { get; set; }
        public double DesignCost { get; set; }
        public double SetupCost { get; set; }
    }
    public class Sra3FormInput : Inputs
    {
        public double Kinds { get; set; }
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
    }
    public class BookletFormInputs : Inputs
    {
        public double Kinds { get; set; }
        public double Pages { get; set; }
        public Guid? FlatSizeId { get; set; }
        public bool CustomFlatSize { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public Guid CoverStockId { get; set; }
        public Guid InnerStockId { get; set; }
        public bool Colour { get; set; }
        public int HolePunches { get; set; }
    }

    public class DocumentFormInputs : Inputs
    {
        public double Kinds { get; set; }
        public double Pages { get; set; }
        public Guid FlatSizeId { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public Guid StockId { get; set; }
        //public Guid? BindingCoildStockId { get; set; }
        public bool DoubleSided { get; set; }
        public bool Colour { get; set; }
        public bool Lamination { get; set; } // Lamination not implemented yet
        public BindingCoilType Binding { get; set; }
        public int Folds { get; set; }
        public int Staples { get; set; }
        public int HolePunches { get; set; }
    }
}
