using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

public class InFilter<T, TProperty> : IFilterOperation<T, TProperty>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, TProperty>> propertySelector, string value)
    {
        if (typeof(TProperty).IsEnum)
        {
            var values = value.Split(',')
                .Select(v => Enum.Parse(typeof(TProperty), v.Trim(), true))
                .Cast<TProperty>()
                .ToArray();

            var inExpression = Expression.Call(
                typeof(Enumerable),
                "Contains",
                new[] { typeof(TProperty) },
                Expression.Constant(values),
                propertySelector.Body);

            return Expression.Lambda<Func<T, bool>>(inExpression, propertySelector.Parameters);
        }
        else
        {
            var values = value.Split(',')
                .Select(v => (TProperty)Convert.ChangeType(v.Trim(), typeof(TProperty)))
                .ToArray();

            var inExpression = Expression.Call(
                typeof(Enumerable),
                "Contains",
                new[] { typeof(TProperty) },
                Expression.Constant(values),
                propertySelector.Body);

            return Expression.Lambda<Func<T, bool>>(inExpression, propertySelector.Parameters);
        }
    }
}