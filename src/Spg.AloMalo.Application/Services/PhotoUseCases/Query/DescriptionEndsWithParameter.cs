using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class DescriptionEndsWithParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public DescriptionEndsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "description")
            {
                if (parts[1]?.Trim().ToLower() == "ew")
                {
                    return _photoFilterBuilder.ApplyDescriptionEndssFilter(parts[2]);
                }
            }
            return _photoFilterBuilder;
        }
    }
}


