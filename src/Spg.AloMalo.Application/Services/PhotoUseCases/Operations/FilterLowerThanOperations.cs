using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterLowerThanOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterLowerThanOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("lt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Width)
                .Use<int>(_photoFilterBuilder.ApplyWidthLowerThanFilter);

            return _photoFilterBuilder;
        }
    }
}
