namespace TechAnswers.Data.Models
{
    public class WorkerOptions : IWorkerOptions
    {
        public string FileName { get; set; }
        public string DateFormat { get; set; }
    }

    public interface IWorkerOptions
    {
        public string FileName { get; set; }
        public string DateFormat { get; set; }
    }
}
