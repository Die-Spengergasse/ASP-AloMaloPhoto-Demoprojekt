using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services
{
    public class PhotoServiceWrapper : IPhotoService
    {
        private readonly IPhotoService _photoService;

        public PhotoServiceWrapper(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public PhotoDto Create(CreatePhotoCommand command)
        {
            return _photoService.Create(command);
        }

        public void Update(Photo photo)
        {
            _photoService.Update(photo);
        }

        public IQueryable<PhotoDto> GetPhotos()
        {
            return _photoService.GetPhotos();
        }

        public IQueryable<PhotoDto> GetWhatEver(PhotoId photoId, AlbumId albumId, PhotographerId photograperId)
        {
            return _photoService.GetWhatEver(photoId, albumId, photograperId);
        }
    }
}
