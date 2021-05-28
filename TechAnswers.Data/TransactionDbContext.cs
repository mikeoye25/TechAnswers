using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;

namespace TechAnswers.Data
{
    public class TransactionDbContext : ITransactionDbContext
    {
        private IMongoDatabase Db { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public TransactionDbContext(IOptions<TransactionDatabaseSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.ConnectionString);
            Db = MongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Db.GetCollection<T>(name);
        }
    }
}
