using System;
using System.Linq.Expressions;

namespace Queryable
{
    public static class ExpressionHelpers
    {
        public static Expression<Func<TType, TRetval>> CombinePredicates<TType, TRetval>(Expression<Func<TType, TRetval>> exp1, Expression<Func<TType, TRetval>> exp2, Func<Expression, Expression, Expression> combineWith)
        {
            var paramSwithcer = new ParameterMatcher(exp2.Parameters, exp1.Parameters);
            var body2 = paramSwithcer.Visit(exp2.Body);
            var body1 = exp1.Body;
            var newBody = combineWith(body1, body2);

            return Expression.Lambda<Func<TType, TRetval>>(newBody, exp1.Parameters);
        }

        public static Expression<Func<TType, bool>> And<TType>(this Expression<Func<TType, bool>> source, Expression<Func<TType, bool>> andWithThis)
        {
            if (source == null)
                return andWithThis;
            if (andWithThis == null)
                return source;
            return CombinePredicates(source, andWithThis, Expression.AndAlso);
        }

        public static Expression<Func<TType, bool>> Or<TType>(this Expression<Func<TType, bool>> source, Expression<Func<TType, bool>> andWithThis)
        {
            if (source == null)
                return andWithThis;
            if (andWithThis == null)
                return source;
            return CombinePredicates(source, andWithThis, Expression.OrElse);
        }

        public static Expression<Func<TType, bool>> Not<TType>(this Expression<Func<TType, bool>> source)
        {
            return Expression.Lambda<Func<TType, bool>>(Expression.NotEqual(source.Body, Expression.Constant(true)), source.Parameters);
        }

        public static Expression<Func<TType, bool>> XOr<TType>(this Expression<Func<TType, bool>> source, Expression<Func<TType, bool>> andWithThis)
        {
            if (source == null)
                return andWithThis;
            if (andWithThis == null)
                return source;
            return CombinePredicates(source, andWithThis, (e1, e2) =>
                {
                    return Expression.IsTrue(
                        Expression.And(
                            Expression.IsTrue(
                                Expression.Or(e1, e2)),
                            Expression.IsFalse(
                                Expression.And(e1, e2)
                                )
                            )
                        );
                });
        }
    }
}