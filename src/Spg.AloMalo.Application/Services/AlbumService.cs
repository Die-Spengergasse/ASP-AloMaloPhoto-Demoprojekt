using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Error;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IReadOnlyAlbumRepository _readOnlyAlbumRepository;

        public AlbumService(IReadOnlyAlbumRepository readOnlyAlbumRepository)
        {
            _readOnlyAlbumRepository = readOnlyAlbumRepository;
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAllOk()
        {
            return new ErrorCheck<IQueryable<AlbumDto>>(
                _readOnlyAlbumRepository
                .GetAll()
                .Select(a => a.ToDto())
            );
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAll400()
        {
            throw new AlbumSerivceException("GetAll400(): Something went wrong!");
        }

        public ErrorCheck<IQueryable<AlbumDto>> GetAll404()
        {
            throw new ArgumentException("GetAll404(): Something went wrong!");
        }
    }
}
