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
                if (filter.StartsWith("contains:"))
                {
                    var value = filter.Substring("contains:".Length);
                    builder.ApplyFilter(new ContainsFilter<Photo>("Name", value));
                }
                else if (filter.StartsWith("equals:"))
                {
                    var value = filter.Substring("equals:".Length);
                    builder.ApplyFilter(new EqualsFilter<Photo>("Name", value));
                }
                else if (filter.StartsWith("startswith:"))
                {
                    var value = filter.Substring("startswith:".Length);
                    builder.ApplyFilter(new StartsWithFilter<Photo>("Name", value));
                }
                else if (filter.StartsWith("endswith:"))
                {
                    var value = filter.Substring("endswith:".Length);
                    builder.ApplyFilter(new EndsWithFilter<Photo>("Name", value));
                }
                else if (filter.StartsWith("containsdigits"))
                {
                    builder.ApplyFilter(new ContainsDigitsFilter<Photo>("Name"));
                }
                else if (filter.StartsWith("greaterthan:"))
                {
                    var value = int.Parse(filter.Substring("greaterthan:".Length));
                    builder.ApplyFilter(new GreaterThanFilter<Photo>("Year", value));
                }
                else if (filter.StartsWith("greaterthanequal:"))
                {
                    var value = int.Parse(filter.Substring("greaterthanequal:".Length));
                    builder.ApplyFilter(new GreaterThanOrEqualFilter<Photo>("Year", value));
                }
            }

            return Task.FromResult(
                builder.Build()
                       .Select(p => p.ToDto())
                       .ToList()
            );
        }
    }
}
