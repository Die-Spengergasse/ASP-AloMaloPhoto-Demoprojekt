using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class InParameter<TProperty> : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public InParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            if (operation.ToLower() == "in")
            {
                var values = value.Split(',').Select(v => (TProperty)Convert.ChangeType(v, typeof(TProperty)));
                return _photoFilterBuilder.InFilter(propertyExpression, values);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
