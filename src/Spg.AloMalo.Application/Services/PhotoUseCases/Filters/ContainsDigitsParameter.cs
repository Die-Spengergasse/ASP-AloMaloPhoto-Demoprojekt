using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class ContainsDigitsParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;

        public ContainsDigitsParameter(string propertyName)
        {
            _propertyName = propertyName;
        }

        public bool Matches(T item)
        {
            // Try to get the value of the property with the specified name, if it isn't there, throw an exception
            var property = typeof(T).GetProperty(_propertyName) ??
                throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");

            // Get the value of the property and check if it is a string, then check if it contains digits using regex
            return property.GetValue(item) is string propertyValue && Regex.IsMatch(propertyValue, @"\d");
        }
    }
}
