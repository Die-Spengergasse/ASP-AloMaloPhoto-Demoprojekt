using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GreaterThanParameter<TProperty> : IQueryParameter where TProperty : IComparable<TProperty>
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public GreaterThanParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            if (operation.ToLower() == "gt")
            {
                var typedValue = (TProperty)Convert.ChangeType(value, typeof(TProperty));
                return _photoFilterBuilder.GreaterThanFilter(propertyExpression, typedValue);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
