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
                (IPhotoFilterBuilder)_photoRepository.FilterBuilder;

            builder = new LastNameContainsParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameBeginsWithParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameEndsWithParameter(builder)
                .Compile(request.Query.Filter);
            builder = new EqualsParameter<string>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new NotEqualsParameter<string>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new StartsWithParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new EndsWithParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new ContainsParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new RegexParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new ContainsDigitsParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new GreaterThanParameter<int>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new LessThanParameter<int>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new InParameter<string>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new NotInParameter<string>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new BetweenParameter<int>(_photoRepository.FilterBuilder).Compile(request.Query.Filter);
            builder = new DateRangeParameter(_photoRepository.FilterBuilder).Compile(request.Query.Filter);

            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }
}

//_photoRepository.FilterBuilder