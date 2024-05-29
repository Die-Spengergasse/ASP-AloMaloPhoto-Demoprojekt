using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class GreaterThanOrEqualFilter<T, TProperty> : IFilterOperation<T, TProperty>
    where TProperty : IComparable<TProperty>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value)
    {
        var convertedValue = (TProperty)Convert.ChangeType(value, typeof(TProperty));
        return Expression.Lambda<Func<T, bool>>(
            Expression.GreaterThanOrEqual(
                propertySelector.Body,
                Expression.Constant(convertedValue)),
            propertySelector.Parameters);
    }
}