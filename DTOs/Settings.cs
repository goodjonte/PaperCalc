using System.IO;
using System.Xml;

namespace PaperCalc.DTOs
{
    public class Settings
    {
        public double FilehandlingCost { get; set; }
        public double MarginMultiplier { get; set; }

        public Settings()
        {
        }
        public void SetSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));
            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier");
            if (fileHandlingCost != null && marginMultiplier != null)
            {
                FilehandlingCost = Convert.ToDouble(fileHandlingCost.InnerText);
                MarginMultiplier = Convert.ToDouble(marginMultiplier.InnerText);
            }
        }
        public void SaveSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));
            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier");
            if (fileHandlingCost != null && marginMultiplier != null)
            {
                fileHandlingCost.InnerText = FilehandlingCost.ToString();
                marginMultiplier.InnerText = MarginMultiplier.ToString();
            }
            doc.Save(string.Concat(path, "/wwwroot/Settings.xml"));
        }
    }
}
