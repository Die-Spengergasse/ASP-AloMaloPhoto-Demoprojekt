using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Filters;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using System;
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
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
        }

        public async Task<List<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Query == null) throw new ArgumentNullException(nameof(request.Query));

            var builder = _photoRepository.FilterBuilder;
            if (builder == null) throw new InvalidOperationException("FilterBuilder is not initialized.");

            var filters = request.Query.Filter.Split(';');
            foreach (var filter in filters)
            {
                var filterParts = filter.Split(' ');
                if (filterParts.Length < 3) continue;

                var property = filterParts[0];
                var operation = filterParts[1];
                var value = string.Join(' ', filterParts.Skip(2));

                switch (operation.ToLower())
                {
                    case "equals":
                        builder = builder.ApplyFilter(new EqualsFilter<Photo>(property, value));
                        break;
                    case "contains":
                        builder = builder.ApplyFilter(new ContainsFilter<Photo>(property, value));
                        break;
                    case "startswith":
                        builder = builder.ApplyFilter(new StartsWithFilter<Photo>(property, value));
                        break;
                    case "endswith":
                        builder = builder.ApplyFilter(new EndsWithFilter<Photo>(property, value));
                        break;
                    case "greaterthan":
                        if (int.TryParse(value, out int gtValue))
                        {
                            builder = builder.ApplyFilter(new GreaterThanFilter<Photo>(property, gtValue));
                        }
                        break;
                    case "greaterthanequal":
                        if (int.TryParse(value, out int gteValue))
                        {
                            builder = builder.ApplyFilter(new GreaterThanOrEqualFilter<Photo>(property, gteValue));
                        }
                        break;
                    case "containsdigits":
                        builder = builder.ApplyFilter(new ContainsDigitsFilter<Photo>(property));
                        break;
                }
            }

            var photos = builder.Build().ToList();

            List<PhotoDto> result = builder
              .Build()
              .Select(r => r.ToDto())
              .ToList();


            return await Task.FromResult(result);
        }

        private Guid ConvertIntToGuid(int id)
        {
            // Erstellen eines neuen Guid basierend auf dem int-Wert
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(id).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
