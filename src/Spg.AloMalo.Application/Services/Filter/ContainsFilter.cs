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

            if (typeof(IEnumerable).IsAssignableFrom(property.Type) && property.Type != typeof(string))
            {
                var elementType = property.Type.IsArray ? property.Type.GetElementType() : property.Type.GetGenericArguments()[0];
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var lambdaParameter = Expression.Parameter(elementType, "y");
                var lambdaBody = Expression.Call(lambdaParameter, containsMethod, Expression.Constant(value));
                var lambda = Expression.Lambda<Func<string, bool>>(lambdaBody, lambdaParameter);

                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(elementType);

                var anyCall = Expression.Call(anyMethod, property, lambda);

                return Expression.Lambda<Func<T, bool>>(anyCall, parameter);
            }
            else
            {
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsCall = Expression.Call(property, containsMethod, Expression.Constant(value));
                return Expression.Lambda<Func<T, bool>>(containsCall, parameter);
            }
        }
    }
}
