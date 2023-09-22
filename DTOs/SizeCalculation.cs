namespace PaperCalc.DTOs
{
    //This class is uesd to calculate things based on the height and width of paper, was originally made for custom sizes but will be used to calculate all sizes, this means flatsize class coulb be made redundant in the future, also will help with testing
    public static class SizeCalculation
    {
        const decimal bleed = 3;
        const decimal artworkGap = 1;
        const decimal sra3HeightPrintableLandscape = 310;
        const decimal sra3WidthPrintableLandscape = 440;
        public static int CalculatePerSra3(double? Height, double? Width)
        {
            decimal heightImposistion = (decimal)Height + bleed + artworkGap;
            decimal widthImposistion = (decimal)Width + bleed + artworkGap;
            decimal landscaperows = Math.Floor(sra3HeightPrintableLandscape / heightImposistion);
            decimal landscapecolums = Math.Floor(sra3WidthPrintableLandscape / widthImposistion);
            decimal portraitrows = Math.Floor(sra3HeightPrintableLandscape / widthImposistion);
            decimal portraicolums = Math.Floor(sra3WidthPrintableLandscape / heightImposistion);
            if (landscaperows * landscapecolums > portraitrows * portraicolums)
            {
                return (int)landscaperows * (int)landscapecolums;
            }

            return (int)portraitrows * (int)portraicolums;
        }

        public static int CalculateCuts(double? Height, double? Width)
        {
            decimal heightImposistion = (decimal)Height + bleed + artworkGap;
            decimal widthImposistion = (decimal)Width + bleed + artworkGap;
            decimal landscaperows = Math.Floor(sra3HeightPrintableLandscape / heightImposistion);
            decimal landscapecolums = Math.Floor(sra3WidthPrintableLandscape / widthImposistion);
            decimal portraitrows = Math.Floor(sra3HeightPrintableLandscape / widthImposistion);
            decimal portraicolums = Math.Floor(sra3WidthPrintableLandscape / heightImposistion);
            bool portrait = landscaperows * landscapecolums > portraitrows * portraicolums ? false : true;
            if (portrait)
            {
                return ((int)portraitrows + (int)portraicolums + 2);
            }
            return ((int)landscaperows + (int)landscapecolums + 2);
        }
    }
}
