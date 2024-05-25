using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> Apply();
    }
}
