using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class EndsWithFilter<T> : IFilterOperation<string>
    {
        public Func<string, bool> GetFilter(string propertyValue)
        {
            return item => item.EndsWith(propertyValue);
        }
    }
}
