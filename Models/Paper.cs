using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Paper
    {
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Display(Name = "GSM")]
        public int? Gsm { get; set; }
        [Display(Name = "Size")]
        public Guid SizeId { get; set; }
        [Display(Name = "Pack Quantity")]
        public double PackQuantity { get; set; }
        [Display(Name = "Pack Cost")]
        public double PackCost { get; set; }
        public double SheetCost
        {
            get { return Math.Ceiling((PackCost / PackQuantity) *  100) / 100 ; }
        }
    }
}
