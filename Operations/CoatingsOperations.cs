using PaperCalc.Models;

namespace PaperCalc.Operations
{
    public static class CoatingsOperations
    {
        public static  double getAverage(PaperCalc.Data.PaperCalcContext _context, Guid? Id)
        {
            List<CoatingsPaper> papers = _context.CoatingsPapers.Where(cp => cp.CoatingId == Id).ToList();
            if (papers.Count == 0)
            {
                return 0;
            }
            if(papers.Count == 1)
            {
                return _context.Paper.Find(papers[0].PaperId).SheetCost;
            }
            double total = 0;
            int count = 0;
            foreach (CoatingsPaper paperCoat in papers)
            {
                Paper paper = _context.Paper.Find(paperCoat.PaperId);
                total = total + paper.SheetCost;
                count++;
            }

            double average = total / count;

            return Math.Round(average, 2);
        }

        public static int getNumberOfPapers(PaperCalc.Data.PaperCalcContext _context, Guid? Id)
        {
            int yup = _context.CoatingsPapers.Where(cp => cp.CoatingId == Id).Count();
            return yup;
        }

    }
}
