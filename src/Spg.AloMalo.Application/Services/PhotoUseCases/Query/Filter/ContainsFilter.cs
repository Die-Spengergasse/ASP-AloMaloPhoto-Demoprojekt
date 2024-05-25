using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public class ContainsFilter<T> : BaseFilter<T>
    {
        public ContainsFilter(string propertyName, string value) : base(propertyName, value) { }

        public override bool Apply(T item)
        {
            var propertyValue = GetPropertyValue(item) as string;
            return propertyValue != null && propertyValue.Contains((string)Value);
        }
    }

}
