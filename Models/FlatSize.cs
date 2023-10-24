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
        public double Area { get { return Height * Width; } }

        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }
        public double MultiplierMin { get; set; }
        public double MultiplierMax { get; set; }
        //Calculates the multiplier base on Regression Slope - Based off flatsize - Need to some how do the same for custom sizes
        public double CalculateMultiplier(double? Quantity)
        {
            if (Quantity == null) return 2;
            double xDif = (double)QuantityMin - (double)QuantityMax;
            double yDif = (double)MultiplierMax - (double)MultiplierMin;
            double slope = yDif / xDif;

            double xMean = ((double)QuantityMin + (double)QuantityMax) / 2;
            double yMean = ((double)MultiplierMin + (double)MultiplierMax) / 2;
            double yIntercept = yMean - (slope * xMean);

            double calculatedMultiplier = slope * (double)Quantity + yIntercept;
            if (calculatedMultiplier > MultiplierMax)
            {
                return MultiplierMax;
            }
            if (calculatedMultiplier < MultiplierMin)
            {
                return MultiplierMin;
            }
            return calculatedMultiplier;
        }
    }
    public enum CalculationType
    {
        Sra3,
        Document,
        Booklet,
        WideFormat,
    }
}
