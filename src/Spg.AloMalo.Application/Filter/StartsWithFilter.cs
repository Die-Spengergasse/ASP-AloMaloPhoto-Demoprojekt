public class StartsWithFilter<T> : Filter<T>
{
    private readonly Func<T, string> _propertySelector;
    private readonly string _value;

    public StartsWithFilter(Func<T, string> propertySelector, string value)
    {
        _propertySelector = propertySelector;
        _value = value;
    }

    public override bool Matches(T item)
    {
        return _propertySelector(item).StartsWith(_value);
    }
}
