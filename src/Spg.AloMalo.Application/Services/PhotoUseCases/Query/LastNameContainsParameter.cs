using Spg.AloMalo.DomainModel.Filter;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class NameContainsParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public NameContainsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter, IPhotoFilterBuilder builder)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "name")
            {
                if (parts[1]?.Trim().ToLower() == "ct")
                {
                    return builder.ApplyFilter(p => p.Name, parts[2], new ContainsFilter<Photo>());
                }
            }
            return builder;
        }
    }
}