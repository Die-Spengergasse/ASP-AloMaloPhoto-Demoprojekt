using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class GreaterThanParameter<T> : IQueryParameter<T>
    {
        private readonly string _propertyName;
        private readonly IComparable _value;

        public GreaterThanParameter(string propertyName, IComparable value)
        {
            _propertyName = propertyName;
            _value = value;
        }

        public bool Matches(T item)
        {
            var property = typeof(T).GetProperty(_propertyName);
            if (property == null) throw new ArgumentException($"Property {_propertyName} not found on {typeof(T)}");
            var propertyValue = property.GetValue(item) as IComparable;
            return propertyValue != null && propertyValue.CompareTo(_value) > 0;
        }
    }
}
