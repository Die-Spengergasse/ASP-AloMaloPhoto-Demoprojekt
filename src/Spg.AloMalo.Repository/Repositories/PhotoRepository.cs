using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository.Repositories
{
    public class PhotoRepository : ReadWriteRepository<Photo, IPhotoFilterBuilder, IPhotoUpdateBuilder>, 
        IWritablePhotoRepository, IReadOnlyPhotoRepository
    {
        public PhotoRepository(PhotoContext photoContext,
            IPhotoFilterBuilder readBuilder,
            IPhotoUpdateBuilder updateBuilder)
                : base(photoContext, readBuilder, updateBuilder)
        { }
    }
}
