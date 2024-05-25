using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository
{
    public class ReadOnlyRepository<TEntity, TFilterBilder>
        : RepositoryBase<TEntity>, IReadOnlyRepository<TFilterBilder>
        where TEntity : class
        where TFilterBilder : IEntityFilterBuilder<TEntity>
    {
        private readonly PhotoContext _context;

        public TFilterBilder FilterBuilder { get; set; }

        public ReadOnlyRepository(
            PhotoContext photoContext,
            TFilterBilder filterBuilder)
                : base(photoContext)
        {
            FilterBuilder = filterBuilder;
        }

        public IQueryable<Photo> GetAllPhotos()
        {
            return _context.Photos;
        }
    }
}
