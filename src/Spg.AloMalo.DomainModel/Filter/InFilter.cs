using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class InFilter<T, TProperty> : IFilterOperation<T, TProperty>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value)
    {
        var values = value.Split(',').Select(v => Convert.ChangeType(v.Trim(), typeof(TProperty))).Cast<TProperty>().ToArray();
        var inExpression = Expression.Call(
            typeof(Enumerable),
            "Contains",
            new[] { typeof(TProperty) },
            Expression.Constant(values),
            propertySelector.Body);
        return Expression.Lambda<Func<T, bool>>(inExpression, propertySelector.Parameters);
    }
}