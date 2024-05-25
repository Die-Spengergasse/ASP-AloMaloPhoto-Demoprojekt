using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository.Repositories
{
    public class PhotoRepository : ReadWriteRepository<Photo, IPhotoFilterBuilder, IPhotoUpdateBuilder>, 
        IWritablePhotoRepository, IReadOnlyPhotoRepository
    {
        private readonly PhotoContext _context;

        public PhotoRepository(PhotoContext? photoContext,
            IPhotoFilterBuilder filterBuilder,
            IPhotoUpdateBuilder updateBuilder)
                : base(photoContext, filterBuilder, updateBuilder)
        { }

        public IQueryable<Photo> GetAllPhotos()
        {
            return _context.Photos;
        }
    }
}
