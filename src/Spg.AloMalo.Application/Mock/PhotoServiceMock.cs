using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Mock
{
    public class PhotoServiceMock : IPhotoService
    {
        public PhotoDto Create(CreatePhotoCommand command)
        {
            // Info:Create aber nicht in die echte DB sondern in eine Fake-Db oder Liste 
            throw new NotImplementedException();
        }

        public IQueryable<PhotoDto> GetPhotos()
        {
            throw new NotImplementedException();
        }

        public IQueryable<PhotoDto> GetWhatEver(PhotoId photoId, AlbumId albumId, PhotographerId photograperId)
        {
            throw new NotImplementedException();
        }

        public void Update(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}
