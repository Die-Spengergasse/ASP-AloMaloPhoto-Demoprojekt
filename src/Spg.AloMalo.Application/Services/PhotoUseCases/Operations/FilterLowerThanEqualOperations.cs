using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterLowerThanEqualOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterLowerThanEqualOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("lte")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Width)
                .Use<int>(_photoFilterBuilder.ApplyWidthLowerThanEqualFilter);

            return _photoFilterBuilder;
        }
    }
}
