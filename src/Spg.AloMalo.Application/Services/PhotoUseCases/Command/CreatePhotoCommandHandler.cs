using MediatR;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Exceptions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Command
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommandModel, CreatePhotoReponseDto>
    {
        private readonly IWritablePhotoRepository _photoRepository;
        private readonly IPhotographerRepository _photographerRepository;

        public CreatePhotoCommandHandler(
            IWritablePhotoRepository photoRepository, 
            IPhotographerRepository photographerRepository)
        {
            _photoRepository = photoRepository;
            _photographerRepository = photographerRepository;
        }

        public Task<CreatePhotoReponseDto> Handle(CreatePhotoCommandModel request, CancellationToken cancellationToken)
        {
            Photographer photographer = _photographerRepository.GetByGuid<Photographer>(request.CreatePhotoCommand.PhotographerId)
                ?? throw PhotoServiceValidationException.FromPhotographerRequired();

            if (string.IsNullOrEmpty(request.CreatePhotoCommand.Name))
            {
                throw PhotoServiceValidationException.FromLastNameRequired();
            }

            DateTime creationTimeStamp = DateTime.Now;
            Guid guid = Guid.NewGuid();
            
            // Mappt ImageType
            ImageTypes imageType;
            switch (request.CreatePhotoCommand.ImageType)
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
                request.CreatePhotoCommand.Name,
                request.CreatePhotoCommand.Description,
                creationTimeStamp, // hmmmm, wird vom Service gesetzt!
                imageType,
                new Location(request.CreatePhotoCommand.Location.Longitude, request.CreatePhotoCommand.Location.Latitude),
                request.CreatePhotoCommand.Width,
                request.CreatePhotoCommand.Height,
                request.CreatePhotoCommand.AiGenerated,
                photographer);

            _photoRepository.Create(newPhoto);

            return Task.FromResult(newPhoto.ToDtoCqs());
        }
    }
}
