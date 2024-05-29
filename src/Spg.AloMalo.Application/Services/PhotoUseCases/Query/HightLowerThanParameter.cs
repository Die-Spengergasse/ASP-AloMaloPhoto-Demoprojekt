using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class HightLowerThanParameter : IQueryParameter
	{
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public HightLowerThanParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "hight")
            {
                if (parts[1]?.Trim().ToLower() == "lt")
                {
                    return _photoFilterBuilder.ApplyHightLowerThan(Int32.Parse(parts[2]));
                }
            }
            return _photoFilterBuilder;
        }
    }
}