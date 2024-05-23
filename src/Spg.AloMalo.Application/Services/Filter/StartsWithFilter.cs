using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class StartsWithFilter<T> : IFilterOperation<string>
    {
        public Func<string, bool> GetFilter(string propertyValue)
        {
            return item => item.StartsWith(propertyValue);
        }
    }
}
