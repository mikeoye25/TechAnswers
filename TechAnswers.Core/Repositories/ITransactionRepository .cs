using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAnswers.Core.Models;
using TechAnswers.Core.ViewModels;

namespace TechAnswers.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<DailyTransactionsPerServiceId>> GetDailyTransactionsPerServiceId(DateTime selectedDate);
        Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetDailyTransactionsPerServiceIdPerDay();
        Task<IEnumerable<MonthlyTransactionsPerServiceIdPerDay>> GetMonthlyTransactionsPerServiceIdPerDay();
        Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetTransactionsPerServiceIdUsingTimeRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<DailyTransactionsPerServiceIdPerClientId>> GetTransactionsPerServiceIdPerClientIdUsingTimeRange(DateTime startDate, DateTime endDate);
    }
}
