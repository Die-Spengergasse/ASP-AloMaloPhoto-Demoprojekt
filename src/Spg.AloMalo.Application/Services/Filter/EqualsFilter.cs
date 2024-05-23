using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class EqualsFilter<T> : IFilterOperation<T>
    {
        public Func<T, bool> GetFilter(string propertyValue)
        {
            return item => item.Equals(propertyValue);
        }
    }
}
