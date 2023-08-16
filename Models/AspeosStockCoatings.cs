using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class AspeosStockCoatings
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public double GetAverage(PaperCalc.Data.PaperCalcContext _context)
        {
            List<CoatingsPaper> papers = _context.CoatingsPapers.Where(cp => cp.CoatingId == Id).ToList();
            if (papers.Count == 0)
            {
                return 0;
            }
            if (papers.Count == 1)
            {
                AspeosStock? item = _context.AspeosStock.Find(papers[0].PaperId);
                return (item?.SheetCost) ?? 0;//if item is null return 0, otherwise return item.SheetCost
            }
            double total = 0;
            int count = 0;
            foreach (CoatingsPaper paperCoat in papers)
            {
                AspeosStock? paper = _context.AspeosStock.Find(paperCoat.PaperId);
                if (paper != null)
                {
                    total += paper.SheetCost;
                    count++;
                }
            }

            double average = total / count;

            return Math.Round(average, 2);
        }

        public int GetNumberOfPapers(PaperCalc.Data.PaperCalcContext _context)
        {
            int yup = _context.CoatingsPapers.Where(cp => cp.CoatingId == Id).Count();
            return yup;
        }
    }
    public class CoatingsPaper
    {
        public Guid Id { get; set; }
        public Guid CoatingId { get; set; }
        public Guid PaperId { get; set; }
    }
}
