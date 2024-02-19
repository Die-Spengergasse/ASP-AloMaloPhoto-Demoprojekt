using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository
{
    public class ReadOnlyRepository<TEntity, TReadBilder>
        : RepositoryBase<TEntity>, IReadOnlyRepository<TReadBilder>
        where TEntity : class
        where TReadBilder : IEntityReaderBuilder<TEntity>
    {
        public TReadBilder ReadBuilder { get; set; }

        public ReadOnlyRepository(
            PhotoContext photoContext,
            TReadBilder readBuilder)
                : base(photoContext)
        {
            ReadBuilder = readBuilder;
        }
    }
}
