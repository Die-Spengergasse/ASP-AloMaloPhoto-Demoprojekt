using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Error;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services
{
    public class AlbumServiceWrapper : IAlbumService
    {
        private readonly IAlbumService _albumService;

        public AlbumServiceWrapper(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAllOk()
        {
            try
            {
                // logging(...)
                return _albumService.GetAllOk();
                // logging(...)
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAll400()
        {
            try
            {
                return _albumService.GetAll400();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAll404()
        {
            try
            {
                return _albumService.GetAll404();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
