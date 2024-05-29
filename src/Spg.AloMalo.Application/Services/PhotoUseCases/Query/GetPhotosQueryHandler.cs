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

            builder = new LastNameContainsParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameBeginsWithParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameEndsWithParameter(builder)
                .Compile(request.Query.Filter);
            // builder = new ...
            string filter = request.Query.Filter;
            string[] parts = filter.Split(' ');

            if (parts.Length == 3)
            {
                string property = parts[0].Trim();
                string operation = parts[1].Trim();
                string value = parts[2].Trim();
                builder = new PhotoPropertyFilter(builder, property, operation, value).Apply();
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
