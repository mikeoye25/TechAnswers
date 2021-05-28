using System;

namespace TechAnswers.Core.ViewModels
{
    public class DailyTransactionsPerServiceIdPerDay
    {
        public string ServiceId { get; set; }

        public int TransactionCount { get; set; }
        public DateTime TransactionDay { get; set; }
    }
}
