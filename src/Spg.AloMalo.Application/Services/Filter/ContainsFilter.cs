using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter

public class ContainsFilter<T> : IFilterOperation<T>
{
    private readonly string _propertyName;
    private readonly string _value;

    public ContainsFilter(string propertyName, string value)
    {
        _propertyName = propertyName;
        _value = value;
    }

    public IQueryable<T> Apply(IQueryable<T> items)
    {
        return items.Where(item => GetPropertyValue(item, _propertyName).Contains(_value, StringComparison.OrdinalIgnoreCase));
    }

    private string GetPropertyValue(T item, string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName);
        return property?.GetValue(item)?.ToString() ?? string.Empty;
    }
}


