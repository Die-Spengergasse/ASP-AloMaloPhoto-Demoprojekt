using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository
{
    public class ReadOnlyRepository<TEntity, TFilterBilder>
        : RepositoryBase<TEntity>, IReadOnlyRepository<TFilterBilder>
        where TEntity : class
        where TFilterBilder : IEntityFilterBuilder<TEntity>
    {
        public TFilterBilder FilterBuilder { get; set; }

        public ReadOnlyRepository(
            PhotoContext photoContext,
            TFilterBilder filterBuilder)
                : base(photoContext)
        {
            FilterBuilder = filterBuilder;
        }
    }
}
