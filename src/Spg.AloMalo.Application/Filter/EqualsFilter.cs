public class EqualsFilter<T, TProperty> : Filter<T>
{
    private readonly Func<T, TProperty> _propertySelector;
    private readonly TProperty _value;

    public EqualsFilter(Func<T, TProperty> propertySelector, TProperty value)
    {
        _propertySelector = propertySelector;
        _value = value;
    }

    public override bool Matches(T item)
    {
        return _propertySelector(item).Equals(_value);
    }
}
