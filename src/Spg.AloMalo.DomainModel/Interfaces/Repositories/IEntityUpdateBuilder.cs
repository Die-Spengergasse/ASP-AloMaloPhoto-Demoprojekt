using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IEntityUpdateBuilder<TEntity>
        where TEntity : class
    {
        public TEntity Entity { get; set; }
        int Save();
    }
}
