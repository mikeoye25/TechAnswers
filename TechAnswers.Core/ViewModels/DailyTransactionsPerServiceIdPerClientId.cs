namespace TechAnswers.Core.ViewModels
{
    public class DailyTransactionsPerServiceIdPerClientId
    {
        public string ServiceId { get; set; }
        public string ClientId { get; set; }
        public int TransactionCount { get; set; }
    }
}
