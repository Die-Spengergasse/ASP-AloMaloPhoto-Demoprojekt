using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class HightLowerThanEqualsParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public HightLowerThanEqualsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "hight")
            {
                if (parts[1]?.Trim().ToLower() == "lte")
                {
                    return _photoFilterBuilder.ApplyHightLowerOrEquals(Int32.Parse(parts[2]));
                }
            }
            return _photoFilterBuilder;
        }
    }
}

