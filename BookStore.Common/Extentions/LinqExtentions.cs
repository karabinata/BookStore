using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.Common.Extensions
{
    public static class LinqExtentions
    {
        private const string OrderDirectionAscending = "ascending";
        private const string OrderByDefault = "Id";
        private const string OrderByDescending = "OrderByDescending";
        private const string OrderByAccending = "OrderBy";

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                          string desc)
        {
            var command = desc.ToLower() == OrderDirectionAscending ? OrderByDescending : OrderByAccending;
            var orderDefault = string.IsNullOrEmpty(orderByProperty) ? OrderByDefault : orderByProperty;
            var type = typeof(TEntity);
            var property = type.GetProperty(orderDefault);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}