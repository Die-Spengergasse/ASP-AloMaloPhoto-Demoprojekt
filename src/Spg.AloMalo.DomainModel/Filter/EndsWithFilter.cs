using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.DomainModel.Interfaces;

public class EndsWithFilter<T> : IFilter<T>
{
    private readonly string _propertyName;
    private readonly string _suffix;

    public EndsWithFilter(string propertyName, string suffix)
    {
        _propertyName = propertyName;
        _suffix = suffix;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);

        // Get the MethodInfo for string.EndsWith
        var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        if (method == null)
        {
            throw new InvalidOperationException("Method 'string.EndsWith' not found.");
        }

        var someValue = Expression.Constant(_suffix, typeof(string));
        var endsWithMethodExp = Expression.Call(property, method, someValue);
        var lambda = Expression.Lambda<Func<T, bool>>(endsWithMethodExp, parameter);

        return query.Where(lambda);
    }
}
