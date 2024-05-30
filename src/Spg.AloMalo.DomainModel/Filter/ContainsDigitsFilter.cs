using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Spg.AloMalo.DomainModel.Interfaces;

public class ContainsDigitsFilter<T> : IFilter<T>
{
    private readonly string _propertyName;

    public ContainsDigitsFilter(string propertyName)
    {
        _propertyName = propertyName;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);

        // Get the MethodInfo for Regex.IsMatch
        var method = typeof(Regex).GetMethod("IsMatch", new[] { typeof(string), typeof(string) });

        if (method == null)
        {
            throw new InvalidOperationException("Method 'Regex.IsMatch' not found.");
        }

        var someValue = Expression.Constant("\\d", typeof(string));
        var containsDigitsMethodExp = Expression.Call(method, property, someValue);
        var lambda = Expression.Lambda<Func<T, bool>>(containsDigitsMethodExp, parameter);

        return query.Where(lambda);
    }
}
