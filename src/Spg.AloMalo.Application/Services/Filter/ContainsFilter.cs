using System.Collections;
using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class ContainsFilter<T> : IFilter<T>
    {
        public Expression<Func<T, bool>> GetFilterExpression(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsCall = Expression.Call(property, containsMethod, Expression.Constant(value));

            if (typeof(IEnumerable).IsAssignableFrom(property.Type))
            {
                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(property.Type.GetGenericArguments()[0]);

                var anyCall = Expression.Call(anyMethod, property, Expression.Lambda(containsCall, parameter));
                return Expression.Lambda<Func<T, bool>>(anyCall, parameter);
            }
            else
            {
                return Expression.Lambda<Func<T, bool>>(containsCall, parameter);
            }
        }
    }
}
