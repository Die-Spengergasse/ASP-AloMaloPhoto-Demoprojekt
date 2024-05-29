using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

public class EqualsFilter<T, TProperty> : IFilterOperation<T, TProperty>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value)
    {
        if (typeof(TProperty) == typeof(Guid))
        {
            var convertedValue = Guid.Parse(value);
            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    propertySelector.Body,
                    Expression.Constant(convertedValue, typeof(Guid))),
                propertySelector.Parameters);
        }
        else
        {
            var convertedValue = (TProperty)Convert.ChangeType(value, typeof(TProperty));
            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    propertySelector.Body,
                    Expression.Constant(convertedValue, typeof(TProperty))),
                propertySelector.Parameters);
        }
    }
}