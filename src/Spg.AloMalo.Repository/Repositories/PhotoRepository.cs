using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Validators.RichTypes;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository.Repositories
{
    public class PhotoRepository : ReadWriteRepository<Photo, IPhotoFilterBuilder, IPhotoUpdateBuilder>, 
        IWritablePhotoRepository, IReadOnlyPhotoRepository
    {
        public PhotoRepository(PhotoContext photoContext,
            IPhotoFilterBuilder filterBuilder,
            IPhotoUpdateBuilder updateBuilder)
                : base(photoContext, filterBuilder, updateBuilder)
        { }
    }
}
