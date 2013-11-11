using System;
using System.Linq.Expressions;

namespace Queryable
{
    public abstract class CompositeQuery<TEntity> : IQueryAdapter<TEntity>
    {
        private readonly IQueryAdapter<TEntity> _other;
        private readonly bool _negateOther;

        protected CompositeQuery(IQueryAdapter<TEntity> other) : this(other, false){
        }

        protected CompositeQuery(IQueryAdapter<TEntity> other, bool negateOther)
        {
            _other = other;
            _negateOther = negateOther;
        }

        public Expression<Func<TEntity, bool>> BuildQuery()
        {
            var otherExpression = _other.BuildQuery();
            if (_negateOther)
                otherExpression = otherExpression.Not();

            switch (Operator)
            {
                case Logical.And:
                    return otherExpression.And(BuildQueryCore());
                case Logical.Or:
                    return otherExpression.Or(BuildQueryCore());
                case Logical.Xor:
                    return otherExpression.XOr(BuildQueryCore());
                default:
                    throw new NotImplementedException("Unknown Logical");
            }
        }

        protected abstract Expression<Func<TEntity, bool>> BuildQueryCore();

        protected virtual Logical Operator
        {
            get
            {
                return Logical.And;
            }
        }
    }
}