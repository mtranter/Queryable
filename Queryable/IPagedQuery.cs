using System;
using System.Linq.Expressions;

namespace Queryable
{
    public interface IPagedQuery<TEntity>
    {
        Expression<Func<TEntity, bool>> BuildPredicate();

        int Page { get; }

        int PageSize { get; }
    }
}