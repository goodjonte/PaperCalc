using System.Xml;

namespace PaperCalc.DTOs
{
    public class Settings
    {
        public double FilehandlingCost { get; set; }
        public double MarginMultiplier { get; set; }

        public Settings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:/Users/Jonte/source/repos/PaperCalc/wwwroot/Settings.xml");
            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier");
            if (fileHandlingCost != null && marginMultiplier != null)
            {
                FilehandlingCost = Convert.ToDouble(fileHandlingCost.InnerText);
                MarginMultiplier = Convert.ToDouble(marginMultiplier.InnerText);
            }
            
        }
        public void SaveSettings()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:/Users/Jonte/source/repos/PaperCalc/wwwroot/Settings.xml");
            XmlNode? fileHandlingCost = doc.SelectSingleNode("/Settings/FilehandlingCost");
            XmlNode? marginMultiplier = doc.SelectSingleNode("/Settings/MarginMultiplier");
            if (fileHandlingCost != null && marginMultiplier != null)
            {
                fileHandlingCost.InnerText = FilehandlingCost.ToString();
                marginMultiplier.InnerText = MarginMultiplier.ToString();
            }
            doc.Save("C:/Users/Jonte/source/repos/PaperCalc/wwwroot/Settings.xml");
        }
    }
}
