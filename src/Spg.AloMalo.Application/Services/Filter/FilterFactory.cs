using System;

namespace Spg.AloMalo.Application.Services.Filter
{
    public static class FilterFactory
    {
        public static IFilterOperation<T> CreateFilterOperation<T>(string operation) where T : IComparable<T>
        {
            switch (operation.ToLower())
            {
                case "equals":
                    return new EqualsFilter<T>();
                case "startswith":
                    return (IFilterOperation<T>)new StartsWithFilter<T>();
                case "endswith":
                    return (IFilterOperation<T>)new StartsWithFilter<T>();
                case "greaterthan":
                    return new GreaterThanFilter<T>();
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}");
            }
        }
    }
}
