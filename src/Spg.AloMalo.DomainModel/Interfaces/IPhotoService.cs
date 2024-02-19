using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces
{
    public interface IPhotoService
    {
        IQueryable<PhotoDto> GetWhatEver(PhotoId photoId, AlbumId albumId, PhotographerId photograperId);
        IQueryable<PhotoDto> GetPhotos();
        Photo Create(CreatePhotoCommand command);
        void Update(Photo photo);
    }
}
