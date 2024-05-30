using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class GreaterThanOrEqualParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly IComparable _value;

        public GreaterThanOrEqualParameter(string propertyName, IComparable value)
        {
            _propertyName = propertyName;
            _value = value;
        }

        public bool Matches(T item)
        {
            // Try to get the value of the property with the specified name, if it isn't there, throw an exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Get the value of the property and check if it is comparable, then check if it is greater than or equal to the specified value
            return property.GetValue(item) is IComparable propertyValue && propertyValue.CompareTo(_value) >= 0;
        }
    }
}
