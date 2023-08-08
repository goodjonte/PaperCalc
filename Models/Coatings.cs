using System.ComponentModel.DataAnnotations;

namespace PaperCalc.Models
{
    public class Coatings
    {
        //public Coatings(Guid id, string name)
        //{
        //    Id = id;
        //    Name = name;
        //}

        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
