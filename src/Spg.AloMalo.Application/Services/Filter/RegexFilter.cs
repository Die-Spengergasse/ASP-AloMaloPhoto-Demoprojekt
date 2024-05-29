using System.Collections;
using System.Linq;
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

            if (typeof(IEnumerable).IsAssignableFrom(property.Type) && property.Type != typeof(string))
            {
                var elementType = property.Type.IsArray ? property.Type.GetElementType() : property.Type.GetGenericArguments()[0];
                var regexIsMatchMethod = typeof(Regex).GetMethod("IsMatch", new[] { typeof(string), typeof(string) });

                var lambdaParameter = Expression.Parameter(elementType, "y");
                var lambdaBody = Expression.Call(regexIsMatchMethod, lambdaParameter, Expression.Constant(value));
                var lambda = Expression.Lambda<Func<string, bool>>(lambdaBody, lambdaParameter);

                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(elementType);

                var anyCall = Expression.Call(anyMethod, property, lambda);

                return Expression.Lambda<Func<T, bool>>(anyCall, parameter);
            }
            else
            {
                var regexIsMatchMethod = typeof(Regex).GetMethod("IsMatch", new[] { typeof(string), typeof(string) });
                var regexIsMatchCall = Expression.Call(regexIsMatchMethod, property, Expression.Constant(value));
                return Expression.Lambda<Func<T, bool>>(regexIsMatchCall, parameter);
            }
        }
    }
}
