using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IUpdateBuilderBase<TEntity, TUpdateBuilder>
            where TEntity : class
            where TUpdateBuilder : class
    {
        TUpdateBuilder WithEntity(TEntity photo);
    }
}
