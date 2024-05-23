using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class RegexFilter<T> : IFilter<T>
    {
        public Expression<Func<T, bool>> GetFilterExpression(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            var regexExpression = Expression.Call(
                typeof(Regex).GetMethod("IsMatch", new[] { typeof(string), typeof(string) }),
                property, Expression.Constant(value)
            );

            if (typeof(IEnumerable).IsAssignableFrom(property.Type))
            {
                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(property.Type.GetGenericArguments()[0]);

                var anyCall = Expression.Call(anyMethod, property, Expression.Lambda(regexExpression, parameter));
                return Expression.Lambda<Func<T, bool>>(anyCall, parameter);
            }
            else
            {
                return Expression.Lambda<Func<T, bool>>(regexExpression, parameter);
            }
        }
    }
}
