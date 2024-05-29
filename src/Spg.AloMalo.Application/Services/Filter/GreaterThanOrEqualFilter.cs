using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter

public class GreaterThanOrEqualFilter<T> : IFilterOperation<T>
{
    private readonly string _propertyName;
    private readonly string _value;

    public GreaterThanOrEqualFilter(string propertyName, string value)
    {
        _propertyName = propertyName;
        _value = value;
    }

    public IQueryable<T> Apply(IQueryable<T> items)
    {
        if (decimal.TryParse(_value, out var decimalValue))
        {
            return items.Where(item => decimal.TryParse(GetPropertyValue(item, _propertyName), out var propValue) && propValue >= decimalValue);
        }
        return items;
    }

    private string GetPropertyValue(T item, string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName);
        return property?.GetValue(item)?.ToString() ?? string.Empty;
    }
}


