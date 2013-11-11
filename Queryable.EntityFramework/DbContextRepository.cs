using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Queryable.EntityFramework
{
    public class DbContextRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly DbContext _context;

        public DbContextRepository(DbContext context)
        {
            _context = context;
        }

        public virtual TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual TEntity Create(TEntity TObject)
        {
            var newEntry = DbSet.Add(TObject);
            return newEntry;
        }

        public virtual void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public virtual IEnumerable<TEntity> Query(IQueryAdapter<TEntity> queryAdapter, int? skip = null, int? take = null)
        {
            var query = queryAdapter.BuildQuery();
            return DbSet.Where(query);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected IDbSet<TEntity> DbSet
        {
            get { return _context.Set<TEntity>(); }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
