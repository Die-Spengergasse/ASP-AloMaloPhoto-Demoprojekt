using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class GreaterThanFilter<T> : IFilter<T>
    {
        public Expression<Func<T, bool>> GetFilterExpression(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            var convertedValue = Convert.ChangeType(value, property.Type);

            var greaterThan = Expression.GreaterThan(property, Expression.Constant(convertedValue));
            return Expression.Lambda<Func<T, bool>>(greaterThan, parameter);
        }
    }
}
