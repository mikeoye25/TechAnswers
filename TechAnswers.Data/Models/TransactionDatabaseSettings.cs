namespace TechAnswers.Data.Models
{
    public class TransactionDatabaseSettings : ITransactionDatabaseSettings
    {
        public string TransactionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITransactionDatabaseSettings
    {
        public string TransactionsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
