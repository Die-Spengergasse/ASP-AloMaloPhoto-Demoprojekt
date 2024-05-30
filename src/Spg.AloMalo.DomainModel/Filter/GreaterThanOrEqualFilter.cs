using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces;

public class GreaterThanOrEqualFilter<T> : IFilter<T>
{
    private readonly string _propertyName;
    private readonly int _value;

    public GreaterThanOrEqualFilter(string propertyName, int value)
    {
        _propertyName = propertyName;
        _value = value;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);
        var constant = Expression.Constant(_value);
        var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(greaterThanOrEqual, parameter);

        return query.Where(lambda);
    }
}
