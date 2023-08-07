using Microsoft.EntityFrameworkCore;

namespace PaperCalc.Models
{
    public class Quote
    {
        public Paper Paper { get; set; } = default!;
        public Guid PaperId { get; set; }
        public Size Size { get; set; } = default!;
        public Guid SizeId { get; set; }
        public Colour Colour { get; set; } = default!;
        public Guid ColourId { get; set; }
        public Clicks Clicks { get; set; } = default!;
        public Guid ClicksId { get; set; }

        public double Quantity { get; set; } = default!;

        public async Task SetObjects(PaperCalc.Data.PaperCalcContext _context)
        {
            Paper = _context.Paper.FirstOrDefault(paper => paper.Id == PaperId);
            Colour = _context.Colour.FirstOrDefault(colour => colour.Id == ColourId);
            SizeId = Paper.SizeId;
            Size = _context.Size.FirstOrDefault(size => size.Id == Paper.SizeId);
            Clicks = _context.Clicks.FirstOrDefault(click => click.ColourId == ColourId && click.SizeId == SizeId);


        }

        public double? GetTotal()
        {
            return Math.Ceiling(((Paper.SheetCost + Clicks.Cost) * Quantity) * 100) / 100;

        }

    }
}
