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
        var constant = Expression.Constant(_substring);
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var containsExpression = Expression.Call(property, containsMethod, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

        return query.Where(lambda);
    }
}

