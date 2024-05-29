using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Operations
{
    public class FilterGreaterThanOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterGreaterThanOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("gt")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.Width)
                .Use<int>(_photoFilterBuilder.ApplyWidthGreaterThanFilter);

            return _photoFilterBuilder;
        }
    }
}
