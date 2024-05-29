using MediatR;
using Spg.AloMalo.Application;
using Spg.AloMalo.Application.Parameter;
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

            builder = new FilterContains(builder).Compile(request.Query.Filter);
            builder = new FilterEndsWith(builder).Compile(request.Query.Filter);
            builder = new FilterStartsWith(builder).Compile(request.Query.Filter);

            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }
}