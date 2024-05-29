using System;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class DescriptionBeginsWithParameter: IQueryParameter
	{
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public DescriptionBeginsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "description")
            {
                if (parts[1]?.Trim().ToLower() == "sw")
                {
                    return _photoFilterBuilder.ApplyDescriptionStartsFilter(parts[2]);
                }
            }
            return _photoFilterBuilder;
        }
    }
}