using MediatR;
using Spg.AloMalo.Application.Operations;
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

            builder = new FilterContainsOperations(builder).Compile(request.Query.Filter);
            builder = new FilterStartsWithOperations(builder).Compile(request.Query.Filter);
            builder = new FilterEndsWithOperations(builder).Compile(request.Query.Filter);
            builder = new FilterStartsWithOperations(builder).Compile(request.Query.Filter);


            var result = builder.Build().Select(r => r.ToDto());

            return Task.FromResult(result);
        }

        
    }
}
