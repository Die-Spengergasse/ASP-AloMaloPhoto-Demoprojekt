using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class NotInParameter<TProperty> : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public NotInParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            if (operation.ToLower() == "nin")
            {
                var values = value.Split(',').Select(v => (TProperty)Convert.ChangeType(v, typeof(TProperty)));
                return _photoFilterBuilder.NotInFilter(propertyExpression, values);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
