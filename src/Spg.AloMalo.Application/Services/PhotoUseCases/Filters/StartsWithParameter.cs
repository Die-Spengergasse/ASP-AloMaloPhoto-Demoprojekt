using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class StartsWithParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly string _prefix;

        public StartsWithParameter(string propertyName, string prefix)
        {
            _propertyName = propertyName;
            _prefix = prefix;
        }

        public bool Matches(T item)
        {
            // Try to get the value of the property with the specified name, if it isn't there, throw an exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Get the value of the property and check if it is a string, then check if it starts with the specified prefix
            return property.GetValue(item) is string propertyValue && propertyValue.StartsWith(_prefix);
        }
    }
}
