using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class EqualsFilter<T, TProperty> : IFilterOperation<T, TProperty>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.Equal(
                propertySelector.Body,
                Expression.Constant(Convert.ChangeType(value, typeof(TProperty)), typeof(TProperty))),
            propertySelector.Parameters);
    }
}