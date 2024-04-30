using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;

namespace Spg.AloMalo.Repository
{
    public class ReadWriteRepository<TEntity, TFilterBilder, TUpdateBuilder> 
        : ReadOnlyRepository<TEntity, TFilterBilder>, IReadWriteRepository<TEntity, TUpdateBuilder>
        where TEntity : class
        where TFilterBilder : class, IEntityFilterBuilder<TEntity>
        where TUpdateBuilder : class, IEntityUpdateBuilder<TEntity>
    {
        public IUpdateBuilderBase<TEntity, TUpdateBuilder> UpdateBuilder { get; }

        public ReadWriteRepository(
            PhotoContext photoContext,
            TFilterBilder filterBuilder,
            TUpdateBuilder updateBuilder)
                : base(photoContext, filterBuilder)
        {
            UpdateBuilder = new UpdateBuilderBase<TEntity, TUpdateBuilder>(updateBuilder);
        }
    }
}
