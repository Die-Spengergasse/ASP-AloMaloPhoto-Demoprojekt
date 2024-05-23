using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;

namespace Spg.AloMalo.Repository
{
    public class ReadOnlyRepository<TEntity, TFilterBuilder>
        : RepositoryBase<TEntity>, IReadOnlyRepository<TEntity, TFilterBuilder>
        where TEntity : class
        where TFilterBuilder : class, IEntityFilterBuilder<TEntity>
    {

        public IFilterBuilderBase<TEntity, TFilterBuilder> FilterBuilder { get; }

        public ReadOnlyRepository(
            PhotoContext photoContext,
            TFilterBuilder filterBuilder)
                : base(photoContext)
        {
            FilterBuilder = new FilterBuilderBase<TEntity, TFilterBuilder>(filterBuilder);
        }
    }
}
