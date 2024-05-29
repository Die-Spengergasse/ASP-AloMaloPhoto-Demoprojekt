using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
	public class HigthIsParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        public HigthIsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "hight")
            {
                if (parts[1]?.Trim().ToLower() == "eq")
                {
                    return _photoFilterBuilder.ApplyHightEquals(Int32.Parse(parts[2]));
                }
            }
            return _photoFilterBuilder;
        }
    }
}

