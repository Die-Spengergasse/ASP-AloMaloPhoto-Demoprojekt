using Spg.AloMalo.DomainModel.Error;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IReadOnlyAlbumRepository _readOnlyAlbumRepository;

        public AlbumService(IReadOnlyAlbumRepository readOnlyAlbumRepository)
        {
            _readOnlyAlbumRepository = readOnlyAlbumRepository;
        }

        public ErrorCheck<IQueryable<Album>> GetAllOk()
        {
            return new ErrorCheck<IQueryable<Album>>(_readOnlyAlbumRepository.GetAll());
        }

        public ErrorCheck<IQueryable<Album>> GetAll400()
        {
            throw new AlbumSerivceException("GetAll400(): Something went wrong!");
        }

        public ErrorCheck<IQueryable<Album>> GetAll404()
        {
            throw new ArgumentException("GetAll404(): Something went wrong!");
        }
    }
}
