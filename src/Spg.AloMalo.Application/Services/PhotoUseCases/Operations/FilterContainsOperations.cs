using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterContainsOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterContainsOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("ct")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>(_photoFilterBuilder.ApplyNameContainsFilter);

            ForProperty(queryParameter, p => p.Description)
                .Use<string>(_photoFilterBuilder.ApplyDescriptionContainsFilter);

            return _photoFilterBuilder;
        }
    }
}
