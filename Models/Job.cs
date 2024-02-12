using PaperCalc.Data;
using PaperCalc.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        //Display id is used just for quote pdf, is the first 6 digits of guid
        public string DisplayId { 
            get {
                string guid = Id.ToString();
                string guidNumbers = "";

                for(int i=0; i < guid.Length; i++)
                {
                    if (char.IsDigit(guid[i]))
                    {
                        guidNumbers = guidNumbers + guid[i];
                    }
                    if (guidNumbers.Length > 5) break;
                }
                return guidNumbers;
            } 
        }
        [Display(Name = "date created")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [Display(Name = "job title")]
        public string JobTitle { get; set; } = default!;
        [Display(Name = "client name")]
        public string ClientName { get; set; } = default!;
        [Display(Name = "business name")]
        public string Buissnessname { get; set; } = default!;

        //List of items for the job - not stored in db
        public List<Sra3InputAndCalc> Sra3Items { get; set; } = new();
        public List<BookletInputAndCalc> BookletItems { get; set; } = new();
        public List<DocumentInputAndCalc> DocumentItems { get; set; } = new();
        public List<WideFormatInputAndCalc> WideFormatItems { get; set; } = new();

        //Gets the items for the job from the database and adds them to the lists
        public void GetItems(PaperCalcContext context, String path)
        {
            //get all items with the matching job id
            var jobsItems = context.InputsForJobs.Where(x => x.JobId == Id).ToList();

            //For every item create a calc dto and add it to the list
            for(int i = 0; i < jobsItems.Count; i++)
            {
                switch (jobsItems[i].CalculationType)
                {
                    case CalculationType.Sra3:
                        var sra3Item = context.Sra3FormInput.Find(jobsItems[i].InputId);
                        if (sra3Item != null)
                        {
                            Sra3Items.Add(new (new (context, path, sra3Item), sra3Item));
                        }
                        break;
                    case CalculationType.Booklet:
                        var bookletItem = context.BookletFormInputs.Find(jobsItems[i].InputId);
                        if (bookletItem != null)
                        {
                            BookletItems.Add(new (new (context, path, bookletItem), bookletItem ));
                        }
                        break;
                    case CalculationType.Document:
                        var docItem = context.DocumentFormInputs.Find(jobsItems[i].InputId);
                        if (docItem != null)
                        {
                            DocumentItems.Add(new (new (context, path, docItem), docItem));
                        }
                        break;
                    case CalculationType.WideFormat:
                        var wideItem = context.WideFormatFormInputs.Find(jobsItems[i].InputId);
                        if (wideItem != null)
                        {
                            WideFormatItems.Add(new(new(context, path, wideItem), wideItem));
                        }
                        break;
                }
            }
        }
    }
}
