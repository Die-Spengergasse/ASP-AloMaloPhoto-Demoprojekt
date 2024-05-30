using MediatR;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Queries;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GetPhotosQueryModel : IRequest<IQueryable<PhotoDto>>
    {
        public GetPhotosQuery Query { get; }

        public GetPhotosQueryModel(GetPhotosQuery query)
        {
            Query = query;
        }
    }
}
