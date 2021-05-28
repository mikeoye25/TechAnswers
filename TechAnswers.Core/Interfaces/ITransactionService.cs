using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAnswers.Core.Models;
using TechAnswers.Core.ViewModels;

namespace TechAnswers.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> Get();
        Task<Transaction> Get(string id);
        Task<IEnumerable<DailyTransactionsPerServiceId>> GetDailyTransactionsPerServiceId(DateTime selectedDate);
        Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetDailyTransactionsPerServiceIdPerDay();
        Task<IEnumerable<MonthlyTransactionsPerServiceIdPerDay>> GetMonthlyTransactionsPerServiceIdPerDay();
        Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetTransactionsPerServiceIdUsingTimeRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<DailyTransactionsPerServiceIdPerClientId>> GetTransactionsPerServiceIdPerClientIdUsingTimeRange(DateTime startDate, DateTime endDate);
        Task<Transaction> CreateTransaction(Transaction newTransaction);
        List<Transaction> ReadCsvFileToTransaction(string path);
    }
}
