using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterGreaterThanEqualOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterGreaterThanEqualOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("gte")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Width)
                .Use<int>(_photoFilterBuilder.ApplyWidthGreaterThanEqualFilter);

            return _photoFilterBuilder;
        }
    }
}
