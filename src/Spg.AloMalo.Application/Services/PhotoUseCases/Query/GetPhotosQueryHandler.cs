using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            var filters = ParseFilters(request.Query.Filter);

            foreach (var filter in filters)
            {
                builder = builder.ApplyFilter(filter);
            }

            var filteredPhotos = builder.Build().ToList();
            return Task.FromResult(filteredPhotos.Select(p => p.ToDto()).ToList());
        }

        private IEnumerable<IFilter<Photo>> ParseFilters(string filterQuery)
        {
            var filters = new List<IFilter<Photo>>();

            if (string.IsNullOrWhiteSpace(filterQuery))
                return filters;

            var parts = filterQuery.Split(' ');

            for (int i = 0; i < parts.Length; i += 3)
            {
                if (i + 2 >= parts.Length)
                {
                    continue;
                }

                var property = parts[i]?.Trim();
                var operation = parts[i + 1]?.Trim().ToLower();
                var value = parts[i + 2]?.Trim();

                if (string.IsNullOrEmpty(property) || string.IsNullOrEmpty(operation) || string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (operation)
                {
                    case "eq":
                        filters.Add(new EqualsFilter<Photo>(property, value));
                        break;
                    case "ct":
                        filters.Add(new ContainsFilter<Photo>(property, value));
                        break;
                    case "bw":
                        filters.Add(new StartsWithFilter<Photo>(property, value));
                        break;
                    case "ew":
                        filters.Add(new EndsWithFilter<Photo>(property, value));
                        break;
                }
            }

            return filters;
        }
    }
}
