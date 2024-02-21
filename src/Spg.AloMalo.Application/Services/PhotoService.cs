using Microsoft.Extensions.Logging;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository;
using Spg.AloMalo.Repository.Extensions;
using Spg.AloMalo.Repository.Repositories;

namespace Spg.AloMalo.Application.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoContext _dbContext;
        private readonly IWritablePhotoRepository _writablePhotoRepository;
        private readonly IReadOnlyPhotoRepository _readOnlyPhotoRepository;
        private readonly IPhotographerRepository _photographerRepository;
        private readonly ILogger<PhotoService> _logger;
        private readonly IDateTimeService _dateTimeService;

        public PhotoService(
            PhotoContext dbContext, 
            ILogger<PhotoService> logger, 
            IReadOnlyPhotoRepository readOnlyPhotoRepository,
            IWritablePhotoRepository photoRepository,
            IPhotographerRepository photographerRepository,
            IDateTimeService dateTimeService)
        {
            _dbContext = dbContext;
            _writablePhotoRepository = photoRepository;
            _readOnlyPhotoRepository = readOnlyPhotoRepository;
            _photographerRepository = photographerRepository;
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public IQueryable<PhotoDto> GetWhatEver(PhotoId photoId, AlbumId albumId, PhotographerId photograperId)
        {
            // LINQ: photoId, albumId, photograperId
            return null!;
        }


        public IQueryable<PhotoDto> GetPhotos()
        {
            IQueryable<PhotoDto> result = _readOnlyPhotoRepository
                .ReadBuilder
                .ApplyNameContainsFilter("My")
                .Build()
                    .Select(p => 
                        new PhotoDto(p.Name, p.Description, p.ImageType, p.Orientation));

            return result;
        }

        public Photo Create(CreatePhotoCommand command)
        {
            _logger.LogDebug("Initalisation");

            _logger.LogDebug("Validation");
            if (string.IsNullOrEmpty(command.Name))
            {
                throw PhotoServiceValidationException.FromLastNameRequired();
            }

            // Erstellungsdatum darf nicht in der Vergangenheit liegen
            if (command.CreationTimeStamp < _dateTimeService.Now)
            {
                throw PhotoServiceValidationException.FromLastNameRequired();
            }

            _logger.LogDebug("Action");

            try
            {
                _logger.LogDebug("Save");

                _logger.LogInformation("Successfully saved");
            }
            catch (Exception)
            {
                _logger.LogError("save failed");
                throw PhotoServiceCreateException.FromSave();
            }

            return null!;
        }

        public void Update(Photo photo)
        {
            // Bad!
            // photos.Update(string name, string description, bool ai, int width, int height, ...)
            // photos.Update(string name)
            // photos.Update(int width, int height, ...)
            // Put, Patch

            var foundPhotographer = _photographerRepository.GetByPK<PhotographerId, Photographer>(new PhotographerId(1));

            Photo foundEntity = _readOnlyPhotoRepository.GetByPK<PhotoId, Photo>(new PhotoId(1)) 
                ?? throw new Exception(); // TODO: Geht besser

            // Better!
            // mit Repository-Pattern
            _writablePhotoRepository.UpdateBuilder
                .WithEntity(foundEntity)
                .WithOrientation(Orientations.Portrait)
                .Save();

            // ohne Repository-Pattern
            _dbContext
                .UpdatePhoto(foundEntity)
                .WithName("New Name")
                .WithDescription("New Description of this Photo (updated)!")
                .WithOrienatation(Orientations.Portrait)
                .Save();
        }
    }
}
