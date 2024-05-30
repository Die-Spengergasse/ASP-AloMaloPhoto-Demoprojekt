using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.DomainModel.Interfaces;

public class ContainsFilter<T> : IFilter<T>
{
    private readonly string _propertyName;
    private readonly string _substring;

    public ContainsFilter(string propertyName, string substring)
    {
        _propertyName = propertyName;
        _substring = substring;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);

        // Get the MethodInfo for string.Contains
        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        if (method == null)
        {
            throw new InvalidOperationException("Method 'string.Contains' not found.");
        }

        var someValue = Expression.Constant(_substring, typeof(string));
        var containsMethodExp = Expression.Call(property, method, someValue);
        var lambda = Expression.Lambda<Func<T, bool>>(containsMethodExp, parameter);

        return query.Where(lambda);
    }
}
