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

        // General
        [Display(Name = "Minimum Job Cost ($ before gst)")]
        public double MinimumJobCost { get; set; }
        [Display(Name = "Kinds Multiplier")]
        public double KindsMultiplier { get; set; }
        [Display(Name = "Gst (% as decimal)")]
        public double Gst { get; set; }

        //Sra3
        [Display(Name = "Buffer Decider")]
        public double Sra3BufferDecider { get; set; }
        [Display(Name = "Buffer Standard")]
        public double Sra3Buffer { get; set; }
        [Display(Name = "Buffer High")]
        public double Sra3BufferHigh { get; set; }

        //Documents
        [Display(Name = "Buffer Decider")]
        public double DocumentsBufferDecider { get; set; }
        [Display(Name = "Buffer Standard")]
        public double DocumentsBuffer { get; set; }
        [Display(Name = "Buffer High")]
        public double DocumentsBufferHigh { get; set; }

        //WideFormat
        [Display(Name = "Cost Per Year")]
        public double InkCostPerYear { get; set; }
        [Display(Name = "A0 Per Year")]
        public double InkA0PerYear { get; set; }
        [Display(Name = "Ink Percent B&W")]
        public double InkPercentBW { get; set; }
        [Display(Name = "Ink Coverage /m")]
        public double InkCoveragePerMeter { get { return InkCostPerYear / (InkA0PerYear * 1189 / 1000);  } }
        [Display(Name = "Buffer Decider")]
        public double WideFormatBufferDecider { get; set; }
        [Display(Name = "Buffer Standard")]
        public double WideFormatBuffer { get; set; }
        [Display(Name = "Buffer High")]
        public double WideFormatBufferHigh { get; set; }

        //Booklets
        [Display(Name = "Buffer Decider")]
        public double BookletsBufferDecider { get; set; }
        [Display(Name = "Buffer Standard")]
        public double BookletsBuffer { get; set; }
        [Display(Name = "Buffer High")]
        public double BookletsBufferHigh { get; set; }

        //Click Rates
        [Display(Name = "A4 B&W Click Rate")]
        public double A4BlackClick { get; set; }
        [Display(Name = "A3 B&W Click Rate")]
        public double A3BlackClick { get { return A4BlackClick * 2; } }
        [Display(Name = "A4 Colour Click Rate")]
        public double A4ColourClick { get; set; }
        [Display(Name = "A3 Colour Click Rate")]
        public double A3ColourClick { get { return A4ColourClick * 2; } }


        //Labour
        [Display(Name = "Hourly Rate")]
        public double HourlyRate { get; set; }

        [Display(Name = "Actions per Hour")]
        public double ManualCuts { get; set; }
        [Display(Name = "Manual Cuts Cost")]
        public double ManualCutsCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / ManualCuts) * 20) / 20; // round up to nearest 0.05
            }
        }
        [Display(Name = "Manual Cuts Charge")]
        public double ManualCutsChargePA {
            get
            {
                return ManualCutsCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double GuillotineCuts { get; set; }
        [Display(Name = "Guillotine Cuts Cost")]
        public double GuillotineCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / GuillotineCuts) * 20) / 20;
            }
        }
        [Display(Name = "Guillotine Cuts Charge")]
        public double GuillotineChargePA {
            get
            {
                return GuillotineCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double ManualDrills { get; set; }
        [Display(Name = "Manual Drills Cost")]
        public double ManualDrillsCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / ManualDrills) * 20) / 20;
            }
        }
        [Display(Name = "Manual Drills Charge")]
        public double ManualDrillsChargePA {
            get
            {
                return ManualDrillsCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double Creases { get; set; }
        [Display(Name = "Creases Cost")]
        public double CreasesCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / Creases) * 20) / 20;
            }
        }
        [Display(Name = "Creases Charge")]
        public double CreasesChargePA {
            get
            {
                return CreasesCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double Folds { get; set; }
        [Display(Name = "Folds Cost")]
        public double FoldsCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / Folds) * 20) / 20;
            }
        }
        [Display(Name = "Folds Charge")]
        public double FoldsChargePA {
            get
            {
                return FoldsCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double BindingPunch { get; set; }
        [Display(Name = "Binding Punch Cost")]
        public double BindingPunchCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / BindingPunch) * 20) / 20;
            }
        }
        [Display(Name = "Binding Punch Charge")]
        public double BindingPunchChargePA {
            get
            {
                return BindingPunchCostPA * 2.5;
            }
        }

        [Display(Name = "Actions per Hour")]
        public double Tape { get; set; }
        [Display(Name = "Double Sided Tape Cost")]
        public double TapeCostPA {
            get
            {
                return Math.Ceiling((HourlyRate / Tape) * 20) / 20;
            }
        }
        [Display(Name = "Double Sided Tape Charge")]
        public double TapeChargePA {
            get
            {
                return TapeCostPA * 2.5;
            }
        }

        public void SetSettings(String path)
        {
            PathForSettings = path;
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? minimumJobCost = doc.SelectSingleNode("/Settings/MinimumJobCost");
            XmlNode? kindsMultiplier = doc.SelectSingleNode("/Settings/KindsMultiplier");
            XmlNode? gst = doc.SelectSingleNode("/Settings/Gst");

            XmlNode? a4BlackClick = doc.SelectSingleNode("/Settings/ClickRates/A4Black");
            XmlNode? a4ColourClick = doc.SelectSingleNode("/Settings/ClickRates/A4Colour");

            XmlNode? sra3BufferDecider = doc.SelectSingleNode("/Settings/Sra3/Buffer/Decider");
            XmlNode? sra3BufferLow = doc.SelectSingleNode("/Settings/Sra3/Buffer/Low");
            XmlNode? sra3BufferHigh = doc.SelectSingleNode("/Settings/Sra3/Buffer/High");

            XmlNode? documentsBufferDecider = doc.SelectSingleNode("/Settings/Document/Buffer/Decider");
            XmlNode? documentsBufferLow = doc.SelectSingleNode("/Settings/Document/Buffer/Low");
            XmlNode? documentsBufferHigh = doc.SelectSingleNode("/Settings/Document/Buffer/High");

            XmlNode? bookletsBufferDecider = doc.SelectSingleNode("/Settings/Booklet/Buffer/Decider");
            XmlNode? bookletsBufferLow = doc.SelectSingleNode("/Settings/Booklet/Buffer/Low");
            XmlNode? bookletsBufferHigh = doc.SelectSingleNode("/Settings/Booklet/Buffer/High");

            XmlNode? wideFormatBufferDecider = doc.SelectSingleNode("/Settings/WideFormat/Buffer/Decider");
            XmlNode? wideFormatBufferLow = doc.SelectSingleNode("/Settings/WideFormat/Buffer/Low");
            XmlNode? wideFormatBufferHigh = doc.SelectSingleNode("/Settings/WideFormat/Buffer/High");

            XmlNode? inkCostPerYear = doc.SelectSingleNode("/Settings/WideFormat/ink/CostPerYear");
            XmlNode? a0PerYear = doc.SelectSingleNode("/Settings/WideFormat/ink/A0PerYear");
            XmlNode? inkPercentageBW = doc.SelectSingleNode("/Settings/WideFormat/ink/PercentageForBW");

            XmlNode? hourlyRate = doc.SelectSingleNode("/Settings/Labour/HourlyRate");
            XmlNode? manualCuts = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/ManualCuts");
            XmlNode? guillotineCuts = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/GuillotineCuts");
            XmlNode? manualDrills = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/ManualDrills");
            XmlNode? creases = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/Creases");
            XmlNode? folds = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/Folds");
            XmlNode? bindingPunch = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/BindingPunch");
            XmlNode? doubleSidedTapeM = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/DoubleSidedTapeM");

            MinimumJobCost = minimumJobCost != null ? Convert.ToDouble(minimumJobCost.InnerText) : 0;
            KindsMultiplier = kindsMultiplier != null ? Convert.ToDouble((string)kindsMultiplier.InnerText) : 0;
            Gst = gst != null ? Convert.ToDouble(((string)gst.InnerText).Trim()) : 0;

            A4BlackClick = a4BlackClick != null ? Convert.ToDouble(a4BlackClick.InnerText) : 0;
            A4ColourClick = a4ColourClick != null ? Convert.ToDouble(a4ColourClick.InnerText) : 0;

            Sra3BufferDecider = sra3BufferDecider != null ? Convert.ToDouble(sra3BufferDecider.InnerText) : 0;
            Sra3Buffer = sra3BufferLow != null ? Convert.ToDouble(sra3BufferLow.InnerText) : 0;
            Sra3BufferHigh = sra3BufferHigh != null ? Convert.ToDouble(sra3BufferHigh.InnerText) : 0;

            DocumentsBufferDecider = documentsBufferDecider != null ? Convert.ToDouble(documentsBufferDecider.InnerText) : 0;
            DocumentsBuffer = documentsBufferLow != null ? Convert.ToDouble(documentsBufferLow.InnerText) : 0;
            DocumentsBufferHigh = documentsBufferHigh != null ? Convert.ToDouble(documentsBufferHigh.InnerText) : 0;

            BookletsBufferDecider = bookletsBufferDecider != null ? Convert.ToDouble(bookletsBufferDecider.InnerText) : 0;
            BookletsBuffer = bookletsBufferLow != null ? Convert.ToDouble(bookletsBufferLow.InnerText) : 0;
            BookletsBufferHigh = bookletsBufferHigh != null ? Convert.ToDouble(bookletsBufferHigh.InnerText) : 0;

            WideFormatBufferDecider = wideFormatBufferDecider != null ? Convert.ToDouble(wideFormatBufferDecider.InnerText) : 0;
            WideFormatBuffer = wideFormatBufferLow != null ? Convert.ToDouble(wideFormatBufferLow.InnerText) : 0;
            WideFormatBufferHigh = wideFormatBufferHigh != null ? Convert.ToDouble(wideFormatBufferHigh.InnerText) : 0;
            InkCostPerYear = inkCostPerYear != null ? Convert.ToDouble(inkCostPerYear.InnerText) : 0;
            InkA0PerYear = a0PerYear != null ? Convert.ToDouble(a0PerYear.InnerText) : 0;
            InkPercentBW = inkPercentageBW != null ? Convert.ToDouble(inkPercentageBW.InnerText) : 0;

            HourlyRate = hourlyRate != null ? Convert.ToDouble(hourlyRate.InnerText) : 0;
            ManualCuts = manualCuts != null ? Convert.ToDouble(manualCuts.InnerText) : 0;
            GuillotineCuts = guillotineCuts != null ? Convert.ToDouble(guillotineCuts.InnerText) : 0;
            ManualDrills = manualDrills != null ? Convert.ToDouble(manualDrills.InnerText) : 0;
            Creases = creases != null ? Convert.ToDouble(creases.InnerText) : 0;
            Folds = folds != null ? Convert.ToDouble(folds.InnerText) : 0;
            BindingPunch = bindingPunch != null ? Convert.ToDouble(bindingPunch.InnerText) : 0;
            Tape = doubleSidedTapeM != null ? Convert.ToDouble(doubleSidedTapeM.InnerText) : 0;
        }
        public void SaveSettings(String path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Concat(path, "/wwwroot/Settings.xml"));

            XmlNode? minimumJobCost = doc.SelectSingleNode("/Settings/MinimumJobCost");
            XmlNode? kindsMultiplier = doc.SelectSingleNode("/Settings/KindsMultiplier");
            XmlNode? gst = doc.SelectSingleNode("/Settings/Gst");

            XmlNode? a4BlackClick = doc.SelectSingleNode("/Settings/ClickRates/A4Black");
            XmlNode? a4ColourClick = doc.SelectSingleNode("/Settings/ClickRates/A4Colour");

            XmlNode? sra3BufferDecider = doc.SelectSingleNode("/Settings/Sra3/Buffer/Decider");
            XmlNode? sra3BufferLow = doc.SelectSingleNode("/Settings/Sra3/Buffer/Low");
            XmlNode? sra3BufferHigh = doc.SelectSingleNode("/Settings/Sra3/Buffer/High");

            XmlNode? documentsBufferDecider = doc.SelectSingleNode("/Settings/Document/Buffer/Decider");
            XmlNode? documentsBufferLow = doc.SelectSingleNode("/Settings/Document/Buffer/Low");
            XmlNode? documentsBufferHigh = doc.SelectSingleNode("/Settings/Document/Buffer/High");

            XmlNode? bookletsBufferDecider = doc.SelectSingleNode("/Settings/Booklet/Buffer/Decider");
            XmlNode? bookletsBufferLow = doc.SelectSingleNode("/Settings/Booklet/Buffer/Low");
            XmlNode? bookletsBufferHigh = doc.SelectSingleNode("/Settings/Booklet/Buffer/High");

            XmlNode? wideFormatBufferDecider = doc.SelectSingleNode("/Settings/WideFormat/Buffer/Decider");
            XmlNode? wideFormatBufferLow = doc.SelectSingleNode("/Settings/WideFormat/Buffer/Low");
            XmlNode? wideFormatBufferHigh = doc.SelectSingleNode("/Settings/WideFormat/Buffer/High");

            XmlNode? inkCostPerYear = doc.SelectSingleNode("/Settings/WideFormat/ink/CostPerYear");
            XmlNode? a0PerYear = doc.SelectSingleNode("/Settings/WideFormat/ink/A0PerYear");
            XmlNode? inkPercentageBW = doc.SelectSingleNode("/Settings/WideFormat/ink/PercentageForBW");

            XmlNode? hourlyRate = doc.SelectSingleNode("/Settings/Labour/HourlyRate");
            XmlNode? manualCuts = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/ManualCuts");
            XmlNode? guillotineCuts = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/GuillotineCuts");
            XmlNode? manualDrills = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/ManualDrills");
            XmlNode? creases = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/Creases");
            XmlNode? folds = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/Folds");
            XmlNode? bindingPunch = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/BindingPunch");
            XmlNode? doubleSidedTapeM = doc.SelectSingleNode("/Settings/Labour/ActionsPerHour/DoubleSidedTapeM");

            if (minimumJobCost != null && kindsMultiplier != null && gst != null &&
                a4BlackClick != null && a4ColourClick != null &&
                sra3BufferDecider != null && sra3BufferLow != null && sra3BufferHigh != null &&
                documentsBufferDecider != null && documentsBufferLow != null && documentsBufferHigh != null &&
                bookletsBufferDecider != null && bookletsBufferLow != null && bookletsBufferHigh != null &&
                wideFormatBufferDecider != null && wideFormatBufferLow != null && wideFormatBufferHigh != null &&
                inkCostPerYear != null && a0PerYear != null && inkPercentageBW != null &&
                hourlyRate != null && manualCuts != null && guillotineCuts != null &&
                manualDrills != null && creases != null && folds != null &&
                bindingPunch != null && doubleSidedTapeM != null)
            {
                minimumJobCost.InnerText = MinimumJobCost.ToString();
                kindsMultiplier.InnerText = KindsMultiplier.ToString();
                gst.InnerText = Gst.ToString();

                a4BlackClick.InnerText = A4BlackClick.ToString();
                a4ColourClick.InnerText = A4ColourClick.ToString();

                sra3BufferDecider.InnerText = Sra3BufferDecider.ToString();
                sra3BufferLow.InnerText = Sra3Buffer.ToString();
                sra3BufferHigh.InnerText = Sra3BufferHigh.ToString();

                documentsBufferDecider.InnerText = DocumentsBufferDecider.ToString();
                documentsBufferLow.InnerText = DocumentsBuffer.ToString();
                documentsBufferHigh.InnerText = DocumentsBufferHigh.ToString();

                bookletsBufferDecider.InnerText = BookletsBufferDecider.ToString();
                bookletsBufferLow.InnerText = BookletsBuffer.ToString();
                bookletsBufferHigh.InnerText = BookletsBufferHigh.ToString();

                wideFormatBufferDecider.InnerText = WideFormatBufferDecider.ToString();
                wideFormatBufferLow.InnerText = WideFormatBuffer.ToString();
                wideFormatBufferHigh.InnerText = WideFormatBufferHigh.ToString();

                inkCostPerYear.InnerText = InkCostPerYear.ToString();
                a0PerYear.InnerText = InkA0PerYear.ToString();
                inkPercentageBW.InnerText = InkPercentBW.ToString();

                hourlyRate.InnerText = HourlyRate.ToString();
                manualCuts.InnerText = ManualCuts.ToString();
                guillotineCuts.InnerText = GuillotineCuts.ToString();
                manualDrills.InnerText = ManualDrills.ToString();
                creases.InnerText = Creases.ToString();
                folds.InnerText = Folds.ToString();
                bindingPunch.InnerText = BindingPunch.ToString();
                doubleSidedTapeM.InnerText = Tape.ToString();
            }
            else
            {
                return;
            }

            doc.Save(string.Concat(path, "/wwwroot/Settings.xml"));
        }
    }
}
