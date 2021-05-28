using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechAnswers.Core;
using TechAnswers.Core.Interfaces;
using TechAnswers.Core.Models;
using TechAnswers.Core.ViewModels;

namespace TechAnswers.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork UnitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> Get()
        {
            return await UnitOfWork.Transactions.Get();
        }

        public async Task<Transaction> Get(string id)
        {
            return await UnitOfWork.Transactions.Get(id);
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceId>> GetDailyTransactionsPerServiceId(DateTime selectedDate)
        {
            return await UnitOfWork.Transactions.GetDailyTransactionsPerServiceId(selectedDate);
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetDailyTransactionsPerServiceIdPerDay()
        {
            return await UnitOfWork.Transactions.GetDailyTransactionsPerServiceIdPerDay();
        }

        public async Task<IEnumerable<MonthlyTransactionsPerServiceIdPerDay>> GetMonthlyTransactionsPerServiceIdPerDay()
        {
            return await UnitOfWork.Transactions.GetMonthlyTransactionsPerServiceIdPerDay();
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetTransactionsPerServiceIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            return await UnitOfWork.Transactions.GetTransactionsPerServiceIdUsingTimeRange(startDate, endDate);
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerClientId>> GetTransactionsPerServiceIdPerClientIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            return await UnitOfWork.Transactions.GetTransactionsPerServiceIdPerClientIdUsingTimeRange(startDate, endDate);
        }

        public async Task<Transaction> CreateTransaction(Transaction newTransaction)
        {
            await UnitOfWork.Transactions.AddAsync(newTransaction);
            return newTransaction;
        }

        public List<Transaction> ReadCsvFileToTransaction(string path)
        {
            var transactions = new List<Transaction>();
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TransactionMap>();
                    transactions = csv.GetRecords<Transaction>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return transactions;
        }
    }
}
