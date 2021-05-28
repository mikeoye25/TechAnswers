using System;

namespace TechAnswers.Core.ViewModels
{
    public class MonthlyTransactionsPerServiceIdPerDay
    {
        public string ServiceId { get; set; }
        public int TransactionCount { get; set; }
        public DateTime TransactionMonth { get; set; }
    }
}
