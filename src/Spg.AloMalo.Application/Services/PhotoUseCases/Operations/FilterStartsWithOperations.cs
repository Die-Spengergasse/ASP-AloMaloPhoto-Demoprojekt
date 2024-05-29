using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterStartsWithOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterStartsWithOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("sw")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Name)
                .Use<string>(_photoFilterBuilder.ApplyNameStartsWithFilter);

            ForProperty(queryParameter, p => p.Description)
                .Use<string>(_photoFilterBuilder.ApplyDescriptionStartsWithFilter);

            return _photoFilterBuilder;
        }
    }
}
