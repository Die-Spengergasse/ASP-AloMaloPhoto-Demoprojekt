using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IEntityReaderBuilder<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> EntityList { get; set; }
        IQueryable<TEntity> Build();
    }
}
