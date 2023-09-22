using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;

namespace PaperCalc.Models
{
    public class FlatSize
    {
        public Guid Id { get; set; }
        public CalculationType ForCalculation { get; set;}
        public required string Name { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }
        public double MultiplierMin { get; set; }
        public double MultiplierMax { get; set; }
    }
    public enum CalculationType
    {
        Aspeos,
        Flat,
        Booklet,
        WideFormat,
    }
}
