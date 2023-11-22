using PaperCalc.Models;

namespace PaperCalc.DTOs
{
    //This class is uesd to calculate things based on the height and width of paper, was originally made for custom sizes but will be used to calculate all sizes, this means flatsize class coulb be made redundant in the future, also will help with testing
    public class CuttingCalculation
    {
        private const double bleed = 3;
        private const double artworkGap = 1;
        private const double sra3HeightPrintableLandscape = 310;
        private const double sra3WidthPrintableLandscape = 440;

        public double PerSra3 { get; set; }
        public double CuttingLabour { get; set; }
        public double CutsRequired { get; set; }
        public bool Portrait { get; set; }
        public double SheetsUsed { get; set; }

        public CuttingCalculation(double Quantity, double HeightOfASheet, double Height, double Width)
        {
            double heightImposistion = Height + bleed + artworkGap;
            double widthImposistion = Width + bleed + artworkGap;
            double landscaperows = Math.Floor(sra3HeightPrintableLandscape / heightImposistion);
            double landscapecolums = Math.Floor(sra3WidthPrintableLandscape / widthImposistion);
            double portraitrows = Math.Floor(sra3HeightPrintableLandscape / widthImposistion);
            double portraicolums = Math.Floor(sra3WidthPrintableLandscape / heightImposistion);

            //Set PerSra3
            if (landscaperows * landscapecolums > portraitrows * portraicolums)
            {
                PerSra3 = (int)landscaperows * (int)landscapecolums;
            }else
            {
                PerSra3 = (int)portraitrows * (int)portraicolums;
            }
            

            //Set Sheets Used
            SheetsUsed = Math.Ceiling(Quantity / PerSra3);

            //Set Portrait
            Portrait = landscaperows * landscapecolums > portraitrows * portraicolums ? false : true;

            //Calcs For Cutting
            //This Calculation is above my head due to lack of knowledge of printing
            double stackHeight = HeightOfASheet * SheetsUsed;
            double horizontalCycles = Math.Ceiling(stackHeight / 35);

            double horizontalCuts = 0;
            double stackHeightAfterHorizontal = 0;
            double verticalCuts = 0;
            if (Portrait)
            {
                horizontalCuts = horizontalCycles * portraitrows + 1;
                stackHeightAfterHorizontal = portraitrows * stackHeight * (horizontalCuts - 1);
            }
            else
            {
                horizontalCuts = horizontalCycles * landscapecolums + 1;
                stackHeightAfterHorizontal = landscapecolums * stackHeight * (horizontalCuts - 1);
            }

            double verticalCycles = Math.Ceiling(stackHeightAfterHorizontal / 35);

            if (Portrait)
            {
                verticalCuts = verticalCycles * (portraicolums + 1);
            }
            else
            {
                verticalCuts = verticalCycles * (landscaperows + 1);
            }

            if(Quantity >= PerSra3)
            {
                CutsRequired = horizontalCuts + verticalCuts;
            }
            else
            {
                CutsRequired = Quantity * 4;
            }
            CuttingLabour = CutsRequired * 2; //Need to update to new labour values
        }
    }
}
