using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;

namespace PaperCalc.DTOs
{
    public class Settings
    {
        [Display(Name = "Filehandling Cost ($)")]
        public double FilehandlingCost { get; set; }
        [Display(Name = "Small & Urgent Fee ($)")]
        public double SmallOrUrgentMinimum { get; set; }
        [Display(Name = "Margin Multiplier")]
        public double MarginMultiplier { get; set; }
        [Display(Name = "For Small Or Urgent Jobs")]
        public double MarginMultiplierSmallOrUrgent { get; set; }
        [Display(Name = "Buffer Multiplier")]
        public double Buffer { get; set; }
        [Display(Name = "For Small Or Urgent Jobs")]
        public double BufferSmallOrUrgent { get; set; }
        [Display(Name = "Creasing Multiplier - For 1")]
        public double Creasing1 { get; set; }
        [Display(Name = "For 2")]
        public double Creasing2 { get; set; }
        [Display(Name = "For 3")]
        public double Creasing3 { get; set; }
        [Display(Name = "Folding Multiplier - For 1")]
        public double Folding1 { get; set; }
        [Display(Name = "For 2")]
        public double Folding2 { get; set; }
        [Display(Name = "For 3")]
        public double Folding3 { get; set; }

        public void SetSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier/Standard");
            XmlNode? marginMultiplierSmallOrUrgent = doc.SelectSingleNode("/Settings/MarginMultiplier/SmallOrUrgent");
            XmlNode? bufferMultiplier = doc.SelectSingleNode("/Settings/Buffer/Standard");
            XmlNode? bufferMultiplierSmallOrUrgent = doc.SelectSingleNode("/Settings/Buffer/SmallOrUrgent");
            XmlNode? smallOrUrgentMinimum = doc.SelectSingleNode("/Settings/SmallOrUrgentMinimum");
            XmlNode? creasing1 = doc.SelectSingleNode("/Settings/Creasing/CreasingOneMultiplier");
            XmlNode? creasing2 = doc.SelectSingleNode("/Settings/Creasing/CreasingTwoMultiplier");
            XmlNode? creasing3 = doc.SelectSingleNode("/Settings/Creasing/CreasingThreeMultiplier");
            XmlNode? folding1 = doc.SelectSingleNode("/Settings/Folding/FoldingOneMultiplier");
            XmlNode? folding2 = doc.SelectSingleNode("/Settings/Folding/FoldingTwoMultiplier");
            XmlNode? folding3 = doc.SelectSingleNode("/Settings/Folding/FoldingThreeMultiplier");

            
            FilehandlingCost = fileHandlingCost != null ? Convert.ToDouble(fileHandlingCost.InnerText) : 0;
            MarginMultiplier = marginMultiplier != null ? Convert.ToDouble(marginMultiplier.InnerText) : 0;
            MarginMultiplierSmallOrUrgent = marginMultiplierSmallOrUrgent != null ? Convert.ToDouble(marginMultiplierSmallOrUrgent.InnerText) : 0;
            Buffer = bufferMultiplier != null ? Convert.ToDouble(bufferMultiplier.InnerText) : 0;
            BufferSmallOrUrgent = bufferMultiplierSmallOrUrgent != null ? Convert.ToDouble(bufferMultiplierSmallOrUrgent.InnerText) : 0;
            SmallOrUrgentMinimum = smallOrUrgentMinimum != null ? Convert.ToDouble(smallOrUrgentMinimum.InnerText) : 0;
            Creasing1 = creasing1 != null ? Convert.ToDouble(creasing1.InnerText) : 0;
            Creasing2 = creasing2 != null ? Convert.ToDouble(creasing2.InnerText) : 0;
            Creasing3 = creasing3 != null ? Convert.ToDouble(creasing3.InnerText) : 0;
            Folding1 = folding1 != null ? Convert.ToDouble(folding1.InnerText) : 0;
            Folding2 = folding2 != null ? Convert.ToDouble(folding2.InnerText) : 0;
            Folding3 = folding3 != null ? Convert.ToDouble(folding3.InnerText) : 0;

        }
        public void SaveSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier/Standard");
            XmlNode? marginMultiplierSmallOrUrgent = doc.SelectSingleNode("/Settings/MarginMultiplier/SmallOrUrgent");
            XmlNode? bufferMultiplier = doc.SelectSingleNode("/Settings/Buffer/Standard");
            XmlNode? bufferMultiplierSmallOrUrgent = doc.SelectSingleNode("/Settings/Buffer/SmallOrUrgent");
            XmlNode? smallOrUrgentMinimum = doc.SelectSingleNode("/Settings/SmallOrUrgentMinimum");
            XmlNode? creasing1 = doc.SelectSingleNode("/Settings/Creasing/CreasingOneMultiplier");
            XmlNode? creasing2 = doc.SelectSingleNode("/Settings/Creasing/CreasingTwoMultiplier");
            XmlNode? creasing3 = doc.SelectSingleNode("/Settings/Creasing/CreasingThreeMultiplier");
            XmlNode? folding1 = doc.SelectSingleNode("/Settings/Folding/FoldingOneMultiplier");
            XmlNode? folding2 = doc.SelectSingleNode("/Settings/Folding/FoldingTwoMultiplier");
            XmlNode? folding3 = doc.SelectSingleNode("/Settings/Folding/FoldingThreeMultiplier");

            if (fileHandlingCost != null && marginMultiplier != null && marginMultiplierSmallOrUrgent != null && bufferMultiplier != null && bufferMultiplierSmallOrUrgent != null && smallOrUrgentMinimum != null && creasing1 != null && creasing2 != null && creasing3 != null && folding1 != null && folding2 != null && folding3 != null)
            {
                fileHandlingCost.InnerText = FilehandlingCost.ToString();
                marginMultiplier.InnerText = MarginMultiplier.ToString();
                marginMultiplierSmallOrUrgent.InnerText = MarginMultiplierSmallOrUrgent.ToString();
                bufferMultiplier.InnerText = Buffer.ToString();
                bufferMultiplierSmallOrUrgent.InnerText = BufferSmallOrUrgent.ToString();
                smallOrUrgentMinimum.InnerText = SmallOrUrgentMinimum.ToString();
                creasing1.InnerText = Creasing1.ToString();
                creasing2.InnerText = Creasing2.ToString();
                creasing3.InnerText = Creasing3.ToString();
                folding1.InnerText = Folding1.ToString();
                folding2.InnerText = Folding2.ToString();
                folding3.InnerText = Folding3.ToString();
            }

            doc.Save(string.Concat(path, "/wwwroot/Settings.xml"));
        }
    }
}
