using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces;

public class EqualsFilter<T> : IFilter<T>
{
    private readonly string _propertyName;
    private readonly object _value;

    public EqualsFilter(string propertyName, object value)
    {
        _propertyName = propertyName;
        _value = value;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, _propertyName);
        var constant = Expression.Constant(_value);
        var equals = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

        return query.Where(lambda);
    }
}
