public class ContainsFilter<T> : Filter<T>
{
    private readonly Func<T, string> _propertySelector;
    private readonly string _value;

    public ContainsFilter(Func<T, string> propertySelector, string value)
    {
        _propertySelector = propertySelector;
        _value = value;
    }

    public override bool Matches(T item)
    {
        return _propertySelector(item).Contains(_value);
    }
}
