using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQueryModel, List<PhotoDto>>
    {
        private readonly IReadOnlyPhotoRepository _photoRepository;

        private readonly string[] _requestOperationsWithTwoFilterParts =
        {
            "containsspecialchars",
            "containsdigits"
        };

        public GetPhotosQueryHandler(IReadOnlyPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public Task<List<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            IPhotoFilterBuilder builder =
                _photoRepository
                .FilterBuilder;

            string filter = request.Query.Filter;
            string[] parts = filter.Split(' ');

            if (parts.Length == 3)
            {
                string property = parts[0].Trim();
                string operation = parts[1].Trim();
                string value = parts[2].Trim();
                builder = new PhotoPropertyFilter(builder, property, operation, value).Apply();
            }
            else if (parts.Length == 2 && _requestOperationsWithTwoFilterParts.Contains(parts[1]))   // parts[1] = operation;
                                                                                                     // if the operation is in _requestOperationsWithTwoFilterParts there are only two parts
            {
                string property = parts[0].Trim();
                string operation = parts[1].Trim();
                builder = new PhotoPropertyFilter(builder, property, operation, null!).Apply();
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
