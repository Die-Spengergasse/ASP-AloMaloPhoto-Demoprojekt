using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> GetFilterExpression(string propertyName, string value);
    }
}
