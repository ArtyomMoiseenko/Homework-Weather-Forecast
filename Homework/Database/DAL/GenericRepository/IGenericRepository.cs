using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework.Database.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        void Create(TEntity item);
        Task<TEntity> FindById(int id);
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
