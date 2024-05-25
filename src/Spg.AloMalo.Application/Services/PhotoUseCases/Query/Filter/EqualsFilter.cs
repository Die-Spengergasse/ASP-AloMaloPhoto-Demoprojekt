using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public class EqualsFilter<T> : BaseFilter<T>
    {
        public EqualsFilter(string propertyName, object value) : base(propertyName, value) { }

        public override bool Apply(T item)
        {
            var propertyValue = GetPropertyValue(item);
            return propertyValue != null && propertyValue.Equals(Value);
        }
    }

}
