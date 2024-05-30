public class FilterService<T>
{
    private readonly List<Filter<T>> _filters;

    public FilterService()
    {
        _filters = new List<Filter<T>>();
    }

    public void AddFilter(Filter<T> filter)
    {
        _filters.Add(filter);
    }

    public IEnumerable<T> ApplyFilters(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            if (_filters.All(f => f.Matches(item)))
            {
                yield return item;
            }
        }
    }
}
