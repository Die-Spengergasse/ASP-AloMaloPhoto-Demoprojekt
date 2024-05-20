using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Filters;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQueryModel, List<PhotoDto>>
    {
        private readonly IReadOnlyPhotoRepository _photoRepository;

        public GetPhotosQueryHandler(IReadOnlyPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public Task<List<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            IPhotoFilterBuilder builder = _photoRepository.FilterBuilder;

            var filters = request.Query.Filter.Split(';');
            foreach (var filter in filters)
            {
                var parts = filter.Split(' ');
                if (parts.Length == 3)
                {
                    var property = parts[0];
                    var operation = parts[1];
                    var value = parts[2];

                    switch (property.ToLower())
                    {
                        case "name":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Name, operation, value).Compile(filter);
                            break;
                        case "description":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Description, operation, value).Compile(filter);
                            break;
                        case "location":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Location, operation, value).Compile(filter);
                            break;
                        case "width":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Width, operation, value).Compile(filter);
                            break;
                        case "height":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Height, operation, value).Compile(filter);
                            break;
                        case "orientation":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.Orientation, operation, value).Compile(filter);
                            break;
                        case "aigenerated":
                            builder = new PropertyFilterParameter<Photo>(builder, p => p.AiGenerated, operation, value).Compile(filter);
                            break;
                        default:
                            throw new InvalidOperationException("Unknown property");
                    }
                }
            }

            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }

}
