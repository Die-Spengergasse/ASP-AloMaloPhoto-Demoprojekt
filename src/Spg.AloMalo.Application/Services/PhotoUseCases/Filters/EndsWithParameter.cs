using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class EndsWithParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly string _suffix;

        public EndsWithParameter(string propertyName, string suffix)
        {
            _propertyName = propertyName;
            _suffix = suffix;
        }

        public bool Matches(T item)
        {
            // Try's to get the value of the property with the specified name, if it isn't there it throws a exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Gets the value of the property and checks if it is a string, after that checks if it ends with the specified suffix
            return property.GetValue(item) is string propertyValue && propertyValue.EndsWith(_suffix);
        }
    }
}
