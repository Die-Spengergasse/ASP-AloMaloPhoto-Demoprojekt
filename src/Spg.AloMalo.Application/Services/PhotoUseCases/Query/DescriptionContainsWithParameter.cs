using System;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class DescriptionContainsWithParameter:IQueryParameter
	{
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public DescriptionContainsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "description")
            {
                if (parts[1]?.Trim().ToLower() == "ct")
                {
                    return _photoFilterBuilder.ApplyDescriptionContainsFilter(parts[2]);
                }
            }
            return _photoFilterBuilder;
        }
    }
}

