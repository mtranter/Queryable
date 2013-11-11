using System;
using System.Linq.Expressions;

namespace Queryable
{
    public interface IQueryAdapter<TEntity>
    {
        Expression<Func<TEntity, bool>> BuildQuery();
    }
}
