using Spg.AloMalo.DomainModel.Filter;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class NameBeginsWithParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public NameBeginsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
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
                    return _photoFilterBuilder.ApplyFilter(p => p.Name, parts[2], new StartsWithFilter<Photo>());
                }
            }
            return _photoFilterBuilder;
        }
    }
}