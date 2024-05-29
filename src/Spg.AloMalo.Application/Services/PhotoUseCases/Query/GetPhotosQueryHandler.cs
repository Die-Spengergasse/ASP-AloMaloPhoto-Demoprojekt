using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Filters;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            IFilterBuilderBase<Photo, IPhotoFilterBuilder> builder =
                (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)_photoRepository.FilterBuilder;

            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>) new EqualsParameter<string>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>) new NotEqualsParameter<string>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>) new StartsWithParameter(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>) new EndsWithParameter(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>) new ContainsParameter(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new RegexParameter(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new ContainsDigitsParameter(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new GreaterThanParameter<int>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new LessThanParameter<int>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new InParameter<string>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new NotInParameter<string>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new BetweenParameter<int>(builder).Compile(request.Query.Filter);
            builder = (IFilterBuilderBase<Photo, IPhotoFilterBuilder>)new DateRangeParameter(builder).Compile(request.Query.Filter);

            return Task.FromResult(
                builder.Build()
                .Select(p => p.ToDto())
                .AsQueryable<PhotoDto>()
            );
        }
    }
}


//_photoRepository.FilterBuilder