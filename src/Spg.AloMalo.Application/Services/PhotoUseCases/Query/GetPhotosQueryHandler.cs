using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
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
        private readonly GeneralFilterParameter _generalFilterParameter;

        public GetPhotosQueryHandler(IReadOnlyPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
            _generalFilterParameter = new GeneralFilterParameter();
        }

        public Task<List<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            IPhotoFilterBuilder builder = _photoRepository.FilterBuilder;

            // Compile filters to expressions
            builder = _generalFilterParameter.Compile(builder, request.Query.Filter);

            // Apply filters at database level
            var filteredPhotos = builder.Build().ToList();
            Console.WriteLine($"Filtered to {filteredPhotos.Count} photos after applying filters.");

            return Task.FromResult(filteredPhotos.Select(p => p.ToDto()).ToList());
        }
    }
}
