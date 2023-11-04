using PaperCalc.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaperCalc.DTOs
{
    [NotMapped]
    public class Sra3InputAndCalc
    {
        public Sra3InputAndCalc(Sra3Calculation calc, Sra3FormInput inputs) {
            Calc = calc;
            Inputs = inputs;
        }
        public Sra3Calculation Calc { get; set; }
        public Sra3FormInput Inputs { get; set; }
    }
    [NotMapped]
    public class DocumentInputAndCalc
    {
        public DocumentInputAndCalc(DocumentCalculation calc, DocumentFormInputs inputs)
        {
            Calc = calc;
            Inputs = inputs;
        }
        public DocumentCalculation Calc { get; set; }
        public DocumentFormInputs Inputs { get; set; }
    }
    [NotMapped]
    public class BookletInputAndCalc
    {
        public BookletInputAndCalc(BookletCalculation calc, BookletFormInputs inputs)
        {
            Calc = calc;
            Inputs = inputs;
        }
        public BookletCalculation Calc { get; set; }
        public BookletFormInputs Inputs { get; set; }
    }
    [NotMapped]
    public class WideFormatInputAndCalc
    {
        public WideFormatInputAndCalc(WideFormatCalculation calc, WideFormatFormInputs inputs)
        {
            Calc = calc;
            Inputs = inputs;
        }
        public WideFormatCalculation Calc { get; set; }
        public WideFormatFormInputs Inputs { get; set; }
    }
}
