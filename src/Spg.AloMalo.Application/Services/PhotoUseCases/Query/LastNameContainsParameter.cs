using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class LastNameContainsParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public LastNameContainsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "name")
            {
                if (parts[1]?.Trim().ToLower() == "ct")
                {
                    return _photoFilterBuilder.ApplyNameContainsFilter(parts[2]);
                }
            }
            return _photoFilterBuilder;
        }
    }
}
