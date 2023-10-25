using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;

namespace PaperCalc.DTOs
{
    public class Settings
    {
        public string? PathForSettings { get; set; }

        //Settings Properties
        [Display(Name = "Minimum Job Cost ($ before gst)")]
        public double MinimumJobCost { get; set; }
        [Display(Name = "Kinds Multiplier")]
        public double KindsMultiplier { get; set; }
        [Display(Name = "Gst (% as decimal)")]
        public double Gst { get; set; }
        [Display(Name = "Buffer Standard")]
        public double Buffer { get; set; }
        [Display(Name = "Buffer High")]
        public double BufferHigh { get; set; }
        [Display(Name = "Creasing Base Price")]
        public double CreasingBase { get; set; }
        [Display(Name = "Folding Base Price")]
        public double FoldingBase { get; set; }
        [Display(Name = "Stapling Base Price")]
        public double StaplingBase { get; set; }
        [Display(Name = "Hole Punching Division Rate")]
        public double HolePunchingBase { get; set; }
        [Display(Name = "Base Click Rate")]
        public double ClickRateBase { get; set; }


        public void SetSettings(String path)
        {
            PathForSettings = path;
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? minimumJobCost = doc.SelectSingleNode("/Settings/MinimumJobCost");

            XmlNode? kindsMultiplier = doc.SelectSingleNode("/Settings/KindsMultiplier");

            XmlNode? buffer = doc.SelectSingleNode("/Settings/Buffer/Standard");
            XmlNode? bufferHigh = doc.SelectSingleNode("/Settings/Buffer/High");

            XmlNode? gst = doc.SelectSingleNode("/Settings/Gst");

            XmlNode? holePunchingBase = doc.SelectSingleNode("/Settings/HolePunching/Base");
            XmlNode? creasingBase = doc.SelectSingleNode("/Settings/Creasing/Base");
            XmlNode? foldingBase = doc.SelectSingleNode("/Settings/Folding/Base");
            XmlNode? staplingBase = doc.SelectSingleNode("/Settings/Stapling/Base");

            XmlNode? clickRateBase = doc.SelectSingleNode("/Settings/ClickRate/Base");

            MinimumJobCost = minimumJobCost != null ? Convert.ToDouble(minimumJobCost.InnerText) : 0;
            KindsMultiplier = kindsMultiplier != null ? Convert.ToDouble((string)kindsMultiplier.InnerText) : 0;
            Buffer = buffer != null ? Convert.ToDouble(((string)buffer.InnerText).Trim()) : 0;
            BufferHigh = bufferHigh != null ? Convert.ToDouble(((string)bufferHigh.InnerText).Trim()) : 0;
            Gst = gst != null ? Convert.ToDouble(((string)gst.InnerText).Trim()) : 0;
            HolePunchingBase = holePunchingBase != null ? Convert.ToDouble(((string)holePunchingBase.InnerText).Trim()) : 0;
            CreasingBase = creasingBase != null ? Convert.ToDouble(((string)creasingBase.InnerText).Trim()) : 0;
            FoldingBase = foldingBase != null ? Convert.ToDouble(((string)foldingBase.InnerText).Trim()) : 0;
            StaplingBase = staplingBase != null ? Convert.ToDouble(((string)staplingBase.InnerText).Trim()) : 0;
            ClickRateBase = clickRateBase != null ? Convert.ToDouble(((string)clickRateBase.InnerText).Trim()) : 0;

        }
        public void SaveSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? minimumJobCost = doc.SelectSingleNode("/Settings/MinimumJobCost");

            XmlNode? kindsMultiplier = doc.SelectSingleNode("/Settings/KindsMultiplier");

            XmlNode? buffer = doc.SelectSingleNode("/Settings/Buffer/Standard");
            XmlNode? bufferHigh = doc.SelectSingleNode("/Settings/Buffer/High");

            XmlNode? gst = doc.SelectSingleNode("/Settings/Gst");

            XmlNode? holePunchingBase = doc.SelectSingleNode("/Settings/HolePunching/Base");
            XmlNode? creasingBase = doc.SelectSingleNode("/Settings/Creasing/Base");
            XmlNode? foldingBase = doc.SelectSingleNode("/Settings/Folding/Base");
            XmlNode? staplingBase = doc.SelectSingleNode("/Settings/Stapling/Base");

            XmlNode? clickRateBase = doc.SelectSingleNode("/Settings/ClickRate/Base");

            if (minimumJobCost != null && kindsMultiplier != null && buffer != null && bufferHigh != null && gst != null && holePunchingBase != null && creasingBase != null && foldingBase != null && staplingBase != null && clickRateBase != null)
            {
                minimumJobCost.InnerText = MinimumJobCost.ToString();
                kindsMultiplier.InnerText = KindsMultiplier.ToString();
                buffer.InnerText = Buffer.ToString();
                bufferHigh.InnerText = BufferHigh.ToString();
                gst.InnerText = Gst.ToString();
                holePunchingBase.InnerText = HolePunchingBase.ToString();
                creasingBase.InnerText = CreasingBase.ToString();
                foldingBase.InnerText = FoldingBase.ToString();
                staplingBase.InnerText = StaplingBase.ToString();
                clickRateBase.InnerText = ClickRateBase.ToString();
            }

            doc.Save(string.Concat(path, "/wwwroot/Settings.xml"));
        }
    }
}
