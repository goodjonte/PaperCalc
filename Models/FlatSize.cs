namespace PaperCalc.Models
{
    public class FlatSize
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
    public class AspeosFlatSize : FlatSize
    {
        public int? PiecesPerSRA3 { get; set; }
        public int? CutsPerSRA3 { get; set; }
        public double? LaminationCost { get; set; }
    }
    public class EpsonFlatSize : FlatSize
    {
        public required string RollLength { get; set; }
        public int CutsPerRoll { get; set; }
    }
    public class FlatFlatSize : FlatSize
    {
        public int? SizeMultiplier { get; set; }
        public double? LaminationCost { get; set; }
    }
}
