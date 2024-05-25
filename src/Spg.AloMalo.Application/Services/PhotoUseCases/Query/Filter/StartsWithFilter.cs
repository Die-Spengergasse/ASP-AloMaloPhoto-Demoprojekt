using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public class StartsWithFilter<T> : BaseFilter<T>
    {
        public StartsWithFilter(string propertyName, string value) : base(propertyName, value) { }

        public override bool Apply(T item)
        {
            var propertyValue = GetPropertyValue(item) as string;
            return propertyValue != null && propertyValue.StartsWith((string)Value);
        }
    }

}
