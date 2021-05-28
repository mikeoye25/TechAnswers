using MongoDB.Driver;

namespace TechAnswers.Data.Interfaces
{
    public interface ITransactionDbContext
    {
        IMongoCollection<Transaction> GetCollection<Transaction>(string name);
    }
}
