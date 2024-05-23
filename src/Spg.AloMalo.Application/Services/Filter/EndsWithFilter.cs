using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class EndsWithFilter<T> : IFilter<T>
    {
        public Expression<Func<T, bool>> GetFilterExpression(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var endsWith = Expression.Call(property, "EndsWith", null, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(endsWith, parameter);
        }
    }
}
