using Microsoft.Extensions.Options;
using TechAnswers.Core;
using TechAnswers.Core.Repositories;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;
using TechAnswers.Data.Repositories;

namespace TechAnswers.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITransactionDbContext Context;
        private ITransactionRepository TransactionRepository;
        private readonly TransactionDatabaseSettings TransactionDatabaseSettings;

        public UnitOfWork(ITransactionDbContext context, IOptions<TransactionDatabaseSettings> transactionDatabaseSettings)
        {
            Context = context;
            TransactionDatabaseSettings = transactionDatabaseSettings.Value;
        }

        public ITransactionRepository Transactions => TransactionRepository = TransactionRepository ?? new TransactionRepository(Context, TransactionDatabaseSettings);
    }
}
