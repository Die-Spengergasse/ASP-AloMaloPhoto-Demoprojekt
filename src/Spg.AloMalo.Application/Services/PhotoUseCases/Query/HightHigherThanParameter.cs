using System;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class HightHigherThanParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public HightHigherThanParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;

        }
        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            //TODO: Checks...
            if (parts[0]?.Trim().ToLower() == "hight")
            {
                if (parts[1]?.Trim().ToLower() == "ht")
                {
                    return _photoFilterBuilder.ApplyHightHigherThan(Int32.Parse(parts[2]));
                }
            }
            return _photoFilterBuilder;
        }
    }
}
