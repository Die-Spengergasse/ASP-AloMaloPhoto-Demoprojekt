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
            Photographer photographer = _photographerRepository.GetByGuid<Photographer>(command.PhotographerId)
                ?? throw PhotoServiceValidationException.FromPhotographerRequired();

            _logger.LogDebug("Validation");
            if (string.IsNullOrEmpty(command.Name))
            {
                throw PhotoServiceValidationException.FromLastNameRequired();
            }

            _logger.LogDebug("Action");

            DateTime creationTimeStamp = DateTime.Now;
            Guid guid = Guid.NewGuid();

            //// Berechnet das Photoformat (Hoch, Breit)
            //// Alternative: Im Model ausrechnen
            //Orientations orientation = Orientations.Portrait;
            //if (command.Width >= command.Height)
            //{
            //    orientation = Orientations.Landscape;
            //}

            // Mappt ImageType
            ImageTypes imageType;
            switch (command.ImageType)
            {
                case ImageTypesDto.Nef:
                    imageType = ImageTypes.Nef;
                    break;
                case ImageTypesDto.Png:
                    imageType = ImageTypes.Png;
                    break;
                case ImageTypesDto.Jpg:
                    imageType = ImageTypes.Jpg;
                    break;
                default:
                    imageType = ImageTypes.Unknown;
                    break;
            }

            // Erstellt neue Entity
            Photo newPhoto = new Photo(
                guid, 
                command.Name, 
                command.Description, 
                creationTimeStamp, // hmmmm, wird vom Service gesetzt!
                imageType, 
                new Location(command.Location.Longitude, command.Location.Latitude), 
                command.Width, 
                command.Height,
                command.AiGenerated, 
                photographer);

            // Save
            try
            {
                _logger.LogDebug("Save");
                _writablePhotoRepository.Create(newPhoto);
                _logger.LogInformation("Successfully saved");
            }
            catch (PhotoRepositoryException ex)
            {
                _logger.LogError("save failed");
                throw PhotoServiceCreateException.FromSave(ex);
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
                .Save();

            // ohne Repository-Pattern
            _dbContext
                .UpdatePhoto(foundEntity)
                .WithName("New Name")
                .WithDescription("New Description of this Photo (updated)!")
                .Save();
        }
    }
}
