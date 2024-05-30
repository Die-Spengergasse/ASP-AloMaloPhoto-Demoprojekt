using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class ContainsParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly string _substring;

        public ContainsParameter(string propertyName, string substring)
        {
            _propertyName = propertyName;
            _substring = substring;
        }

        public bool Matches(T item)
        {
            // Try to get the value of the property with the specified name, if it isn't there, throw an exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Get the value of the property and check if it is a string, then check if it contains the specified substring
            return property.GetValue(item) is string propertyValue && propertyValue.Contains(_substring);
        }
    }
}
