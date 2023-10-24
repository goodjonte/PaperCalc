﻿using PaperCalc.Data;
using PaperCalc.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        [Display(Name = "job title")]
        public string JobTitle { get; set; } = default!;
        [Display(Name = "client name")]
        public string ClientName { get; set; } = default!;
        [Display(Name = "business name")]
        public string Buissnessname { get; set; } = default!;

        //List of items for the job - not stored in db
        public List<Sra3InputAndCalc> Sra3Items { get; set; } = new();
        public List<BookletCalculation> BookletItems { get; set; } = new();
        public List<DocumentCalculation> DocumentItems { get; set; } = new();

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
                            BookletItems.Add(new (context, path, bookletItem));
                        }
                        break;
                    case CalculationType.Document:
                        var docItem = context.DocumentFormInputs.Find(jobsItems[i].InputId);
                        if (docItem != null)
                        {
                            DocumentItems.Add(new (context, path, docItem));
                        }
                        break;

                }
            }
        }
    }
}
