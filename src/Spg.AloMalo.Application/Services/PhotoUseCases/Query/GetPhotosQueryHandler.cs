using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

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
            IPhotoFilterBuilder builder =
                _photoRepository
                .FilterBuilder;

            builder = new PhotoBeginsWithHandler(builder).WithQuery(request.Query.Filter);
            builder = new PhotoContainsHandler(builder).WithQuery(request.Query.Filter);
            builder = new PhotoEndsWithHandler(builder).WithQuery(request.Query.Filter);
            builder = new PhotoGreaterThanFilterHandler(builder).WithQuery(request.Query.Filter);
            builder = new PhotoLowerThanHandler(builder).WithQuery(request.Query.Filter);
            builder = new PhotoPlusMinus100Handler(builder).WithQuery(request.Query.Filter);
            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }
}
