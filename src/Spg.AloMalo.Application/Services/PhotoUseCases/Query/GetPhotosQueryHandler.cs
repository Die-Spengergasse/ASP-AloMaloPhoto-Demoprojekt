using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System.Linq;
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
            IQueryable<Photo> query = _photoRepository.FilterBuilder.Build();

            var filterParts = request.Query.Filter.Split(' ');
            if (filterParts.Length == 3)
            {
                switch (filterParts[0].ToLower())
                {
                    case "name":
                        switch (filterParts[1].ToLower())
                        {
                            case "ct":
                                query = query.Where(p => p.Name.Contains(filterParts[2]));
                                break;
                            case "bw":
                                query = query.Where(p => p.Name.StartsWith(filterParts[2]));
                                break;
                            case "ew":
                                query = query.Where(p => p.Name.EndsWith(filterParts[2]));
                                break;
                        }
                        break;
                    case "description":
                        if (filterParts[1].ToLower() == "ct")
                        {
                            query = query.Where(p => p.Description.Contains(filterParts[2]));
                        }
                        break;
                    case "width":
                        if (filterParts[1] == "gt")
                        {
                            query = query.Where(p => p.Width > int.Parse(filterParts[2]));
                        }
                        break;
                    case "height":
                        if (filterParts[1] == "lt")
                        {
                            query = query.Where(p => p.Height < int.Parse(filterParts[2]));
                        }
                        break;
                }
            }

            return Task.FromResult(query.Select(p => p.ToDto()).ToList());
        }
    }
}