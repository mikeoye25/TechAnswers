using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechAnswers.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(string id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity, string id);
        void Remove(string id);
    }
}
