using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class LastNameBeginsWithParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public LastNameBeginsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "name")
            {
                if (parts[1]?.Trim().ToLower() == "bw")
                {
                    return _photoFilterBuilder.ApplyNameBeginsWithFilter(parts[2]);
                }
            }
            return _photoFilterBuilder;
        }
    }
}
