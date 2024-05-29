using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class StartsWithFilter<T> : IFilterOperation<T, string>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, string>> propertySelector, string value)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.Call(propertySelector.Body, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, Expression.Constant(value)),
            propertySelector.Parameters);
    }
}