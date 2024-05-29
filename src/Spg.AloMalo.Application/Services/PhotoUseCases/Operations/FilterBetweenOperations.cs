using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class FilterBetweenOperations : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public FilterBetweenOperations(IPhotoFilterBuilder photoFilterBuilder)
            : base("bw")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string? queryParameter)
        {
            ForProperty(queryParameter, p => p.CreationTimeStamp)
                .Use<DateTime, DateTime>(_photoFilterBuilder.ApplyCreationTimeStampBetweenFilter);

            return _photoFilterBuilder;
        }
    }
}
