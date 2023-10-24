namespace PaperCalc.Models
{
    public class InputsForJobs
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public Guid InputId { get; set; }
        public CalculationType CalculationType { get; set; }
    }

}
