using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.DomainModel.Interfaces;

public class StartsWithFilter<T> : IFilter<T>
{
    private readonly string _propertyName;
    private readonly string _prefix;

    public StartsWithFilter(string propertyName, string prefix)
    {
        _propertyName = propertyName;
        _prefix = prefix;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);

        // Get the MethodInfo for string.StartsWith
        var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        if (method == null)
        {
            throw new InvalidOperationException("Method 'string.StartsWith' not found.");
        }

        var someValue = Expression.Constant(_prefix, typeof(string));
        var startsWithMethodExp = Expression.Call(property, method, someValue);
        var lambda = Expression.Lambda<Func<T, bool>>(startsWithMethodExp, parameter);

        return query.Where(lambda);
    }
}
