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
}
