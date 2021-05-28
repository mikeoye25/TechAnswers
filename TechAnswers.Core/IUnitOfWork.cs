using TechAnswers.Core.Repositories;

namespace TechAnswers.Core
{
    public interface IUnitOfWork
    {
        ITransactionRepository Transactions { get; }
    }
}
