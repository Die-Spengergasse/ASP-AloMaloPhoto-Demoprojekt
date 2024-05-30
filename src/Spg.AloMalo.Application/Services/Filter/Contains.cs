using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.Filter
{
    public class Contains<T> where T : class
    {
        private IEntityFilterBuilder<T> _builder;

        public Contains(IEntityFilterBuilder<T> builder)
        {
            _builder = builder;
        }
    }
}
