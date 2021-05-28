using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAnswers.Core.Repositories;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;

namespace TechAnswers.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ITransactionDbContext TransactionDbContext;
        protected IMongoCollection<TEntity> DbCollection;

        public Repository(ITransactionDbContext transactionDbContext, ITransactionDatabaseSettings settings)
        {
            TransactionDbContext = transactionDbContext;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            DbCollection = database.GetCollection<TEntity>(settings.TransactionsCollectionName);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var all = await DbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<TEntity> Get(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await DbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            await DbCollection.InsertOneAsync(entity);
        }

        public virtual void Update(TEntity entity, string id)
        {
            var objectId = new ObjectId(id);
            DbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId), entity);
        }

        public void Remove(string id)
        {
            var objectId = new ObjectId(id);
            DbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
        }
    }
}
