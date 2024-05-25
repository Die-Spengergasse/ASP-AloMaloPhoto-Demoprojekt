using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public abstract class BaseFilter<T> : IFilter<T>
    {
        public string PropertyName { get; }
        public object Value { get; }

        protected BaseFilter(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public abstract bool Apply(T item);

        protected object? GetPropertyValue(T item)
        {
            if (item != null)
                return item.GetType().GetProperty(PropertyName)?.GetValue(item, null);

            else
                throw new NullReferenceException("Item can not be null.");
        }
    }
}
