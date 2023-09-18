namespace PaperCalc.DTOs
{
    public class Imposisition
    {
        const decimal bleed = 3;
        const decimal artworkGap = 1;
        const decimal sra3HeightPrintableLandscape = 310;
        const decimal sra3WidthPrintableLandscape = 440;
        private decimal heightImposistion { get; set; }
        private decimal widthImposistion { get; set; }
        private decimal landscaperows { get; set; }
        private decimal landscapecolums { get; set; }
        private decimal portraitrows { get; set; }
        private decimal portraicolums { get; set; }
        private bool portrait { get {
                return landscaperows * landscapecolums > portraitrows * portraicolums ? false : true;
        }}
        public void Calculate(int Height, int Width)
        {
            heightImposistion = (decimal)Height + bleed + artworkGap;
            widthImposistion = (decimal)Width + bleed + artworkGap;
            landscaperows = Math.Floor(sra3HeightPrintableLandscape / heightImposistion);
            landscapecolums = Math.Floor(sra3WidthPrintableLandscape / widthImposistion);
            portraitrows = Math.Floor(sra3HeightPrintableLandscape / widthImposistion);
            portraicolums = Math.Floor(sra3WidthPrintableLandscape / heightImposistion);
        }

        public int PerSra3CustomSize
        {
            get
            {
                if (landscaperows * landscapecolums > portraitrows * portraicolums)
                {
                    return (int)landscaperows * (int)landscapecolums;
                }

                return (int)portraitrows * (int)portraicolums;
            }
        }

        public int CutsCustomSize
        {
            get
            {
                if (portrait)
                {
                    return ((int)portraitrows + (int)portraicolums + 2) * 2;
                }
                return ((int)landscaperows + (int)landscapecolums + 2) * 2;
            }
        }
    }
}
