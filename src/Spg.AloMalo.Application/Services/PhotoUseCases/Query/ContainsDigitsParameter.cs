using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class ContainsDigitsParameter : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public ContainsDigitsParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<string>(queryParameter);
            if (operation.ToLower() == "containsdigits")
            {
                return _photoFilterBuilder.ContainsDigitsFilter(propertyExpression);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
