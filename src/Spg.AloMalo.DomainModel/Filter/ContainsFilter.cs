using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class ContainsFilter<T> : IFilterOperation<T, string>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, string>> propertySelector, string value)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.Call(propertySelector.Body, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, Expression.Constant(value)),
            propertySelector.Parameters);
    }
}