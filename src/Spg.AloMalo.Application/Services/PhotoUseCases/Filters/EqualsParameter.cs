using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class EqualsParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly object _value;

        public EqualsParameter(string propertyName, object value)
        {
            _propertyName = propertyName;
            _value = value;
        }

        public bool Matches(T item)
        {
            // Try to get the value of the property with the specified name, if it isn't there, throw an exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Get the value of the property and check if it equals the specified value
            var propertyValue = property.GetValue(item);
            return propertyValue != null && propertyValue.Equals(_value);
        }
    }
}
