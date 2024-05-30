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

            string filter = request.Query.Filter;
            string[] parts = filter.Split(' ');

            if(parts.Length == 3)
            {
                string property = parts[0].Trim();
                string operation = parts[1].TrimEnd();
                string value = parts[2].TrimEnd();
                builder = new GetPhotoFilter(builder, property, operation, value).Apply();
            }
            else if (parts.Length == 2 && new string[]
                { "containsspecialchars",
                    "containsdigits" }
                .Contains(parts[1]))
            {
                string property = parts[0].Trim();
                string operation = parts[1].TrimEnd();
                builder = new GetPhotoFilter(builder, property, operation, null!).Apply();
            }

            return Task.FromResult(
                builder .Build().Select(p => p.ToDto()).ToList()
            );
        }
    }
}
