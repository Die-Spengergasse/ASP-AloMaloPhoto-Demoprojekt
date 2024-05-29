using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query.Handler;
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

            /*builder = new LastNameContainsParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameBeginsWithParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameEndsWithParameter(builder)
                .Compile(request.Query.Filter);*/
            
            // builder = new ...
            builder = new PhotoEqualsHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoBeginsWithHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoContainsHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoEndsWithHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoLessThanHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoLessThanEqualsHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoGreaterThanEqualsHandler(builder, request.Query.Filter).Builder;
            builder = new PhotoGreaterThanHandler(builder, request.Query.Filter).Builder;

            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }
}
