using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class HightHigherThanEqualsParameter: IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public HightHigherThanEqualsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;

        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "hight")
            {
                if (parts[1]?.Trim().ToLower() == "hte")
                {
                    return _photoFilterBuilder.ApplyHightHigherOrEquals(Int32.Parse(parts[2]));
                }
            }
            return _photoFilterBuilder;
        }
    }
}


