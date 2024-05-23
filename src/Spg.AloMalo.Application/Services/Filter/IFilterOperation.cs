using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public interface IFilterOperation<T>
    {
        Func<T, bool> GetFilter(string propertyValue);
    }

}