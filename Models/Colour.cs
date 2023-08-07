using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Colour
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
