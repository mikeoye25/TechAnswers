using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAnswers.Core.Extensions;
using TechAnswers.Core.Models;
using TechAnswers.Core.Repositories;
using TechAnswers.Core.ViewModels;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;

namespace TechAnswers.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ITransactionDbContext context, ITransactionDatabaseSettings settings) 
            : base(context, settings)
        {
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceId>> GetDailyTransactionsPerServiceId(DateTime selectedDate)
        {
            return await DbCollection
                .Aggregate()
                .Match(Builders<Transaction>.Filter.Gte(u => u.TransactionTime, selectedDate)
                & Builders<Transaction>.Filter.Lte(u => u.TransactionTime, selectedDate.ToEndOfDay()))
                .Group(i => i.ServiceId,
                    g => new
                    {
                        serviceId = g.Key,
                        transactionCount = g.Count()
                    })
                .Project(group => new DailyTransactionsPerServiceId
                {
                    ServiceId = group.serviceId,
                    TransactionCount = group.transactionCount
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetDailyTransactionsPerServiceIdPerDay()
        {
            return await DbCollection
                .Aggregate()
                .Group(i => new 
                    { 
                        i.ServiceId,
                        TransactionTime = new DateTime(i.TransactionTime.Year, i.TransactionTime.Month, i.TransactionTime.Day) 
                    },
                    g => new
                    {
                        Location = g.Key,
                        Count = g.Count()
                    })
                .Project(group => new DailyTransactionsPerServiceIdPerDay
                {
                    ServiceId = group.Location.ServiceId,
                    TransactionCount = group.Count,
                    TransactionDay = group.Location.TransactionTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MonthlyTransactionsPerServiceIdPerDay>> GetMonthlyTransactionsPerServiceIdPerDay()
        {
            return await DbCollection
                .Aggregate()
                .Group(i => new
                {
                    i.ServiceId,
                    TransactionTime = new DateTime(i.TransactionTime.Year, i.TransactionTime.Month, 1)
                },
                    g => new
                    {
                        Location = g.Key,
                        Count = g.Count()
                    })
                .Project(group => new MonthlyTransactionsPerServiceIdPerDay
                {
                    ServiceId = group.Location.ServiceId,
                    TransactionCount = group.Count,
                    TransactionMonth = group.Location.TransactionTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerDay>> GetTransactionsPerServiceIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            return await DbCollection
                .Aggregate()
                .Match(Builders<Transaction>.Filter.Gte(u => u.TransactionTime, startDate)
                & Builders<Transaction>.Filter.Lte(u => u.TransactionTime, endDate.ToEndOfDay()))
                .Group(i => new
                {
                    i.ServiceId,
                    TransactionTime = new DateTime(i.TransactionTime.Year, i.TransactionTime.Month, i.TransactionTime.Day)
                },
                    g => new
                    {
                        Location = g.Key,
                        Count = g.Count()
                    })
                .Project(group => new DailyTransactionsPerServiceIdPerDay
                {
                    ServiceId = group.Location.ServiceId,
                    TransactionCount = group.Count,
                    TransactionDay = group.Location.TransactionTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DailyTransactionsPerServiceIdPerClientId>> GetTransactionsPerServiceIdPerClientIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            return await DbCollection
                .Aggregate()
                .Match(Builders<Transaction>.Filter.Gte(u => u.TransactionTime, startDate)
                & Builders<Transaction>.Filter.Lte(u => u.TransactionTime, endDate.ToEndOfDay()))
                .Group(i => new { i.ServiceId, i.ClientId },
                    g => new
                    {
                        Location = g.Key,
                        Count = g.Count()
                    })
                .Project(group => new DailyTransactionsPerServiceIdPerClientId
                {
                    ServiceId = group.Location.ServiceId,
                    ClientId = group.Location.ClientId,
                    TransactionCount = group.Count
                })
                .ToListAsync();
        }
    }
}
