using System.Collections.Generic;

namespace Queryable
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Query(IQueryAdapter<TEntity> query, int? skip = null, int? take = null);
        void SaveChanges();
    }
}