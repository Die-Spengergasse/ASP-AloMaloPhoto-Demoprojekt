using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Operations;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQueryModel, IQueryable<PhotoDto>>
    {
        private readonly IReadOnlyPhotoRepository _photoRepository;

        public GetPhotosQueryHandler(IReadOnlyPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public Task<IQueryable<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            IPhotoFilterBuilder builder =
               _photoRepository
               .FilterBuilder;

            List<IQueryParameter> operations =
            [
                new StartsWithOperation(builder),
                new ContainsOperation(builder),
                new EqualsOperation(builder),
                new GreaterThanOperation(builder),
                new GreaterThanEqualOperation(builder),
                new LowerThanOperation(builder),
                new LowerThanEqualOperation(builder)
            ];
            foreach (IQueryParameter operation in operations)
            {
                builder = operation.Compile(request?.Query?.Filter ?? string.Empty);
            }

            IQueryable<PhotoDto> result = builder
                .Build()
                .Select(r => r.ToDto());

            return Task.FromResult(result);
        }
    }
}
