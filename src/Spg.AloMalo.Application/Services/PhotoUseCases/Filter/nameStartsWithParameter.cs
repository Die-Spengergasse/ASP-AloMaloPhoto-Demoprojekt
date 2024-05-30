using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Filter;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filter
{
    public class NameStartsWithParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public NameStartsWithParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder ?? throw new ArgumentNullException(nameof(photoFilterBuilder));
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            if (string.IsNullOrWhiteSpace(queryParameter))
                throw new ArgumentException("Query parameter cannot be null or whitespace.", nameof(queryParameter));

            var parts = queryParameter.Split(' ');
            if (parts.Length == 3 && parts[0].Trim().ToLower() == "name" && parts[1].Trim().ToLower() == "startswith")
            {
                return _photoFilterBuilder.ApplyFilter(new StartsWithFilter<Photo>(p => p.Name, parts[2]));
            }
            return _photoFilterBuilder;
        }
    }
}
