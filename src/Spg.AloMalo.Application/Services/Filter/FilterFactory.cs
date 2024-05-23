namespace Spg.AloMalo.Application.Services.Filter
{
    public static class FilterFactory<T>
    {
        public static IFilter<T> GetFilter(string operation)
        {
            switch (operation.ToLower())
            {
                case "equals":
                    return new EqualsFilter<T>();
                case "startswith":
                    return new StartsWithFilter<T>();
                case "endswith":
                    return new EndsWithFilter<T>();
                case "greaterthan":
                    return new GreaterThanFilter<T>();
                case "contains":
                    return new ContainsFilter<T>();
                default:
                    throw new NotSupportedException($"Operation '{operation}' is not supported.");
            }
        }
    }
}
