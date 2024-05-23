using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class RegexParameter : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public RegexParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<string>(queryParameter);
            if (operation.ToLower() == "regex")
            {
                return _photoFilterBuilder.RegexFilter(propertyExpression, value);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
