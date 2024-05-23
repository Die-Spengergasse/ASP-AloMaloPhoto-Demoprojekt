using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class NotEqualsParameter<TProperty> : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public NotEqualsParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            if (operation.ToLower() == "neq")
            {
                var typedValue = (TProperty)Convert.ChangeType(value, typeof(TProperty));
                return _photoFilterBuilder.NotEqualsFilter(propertyExpression, typedValue);
            }
            return (IPhotoFilterBuilder) _photoFilterBuilder;
        }
    }
}
