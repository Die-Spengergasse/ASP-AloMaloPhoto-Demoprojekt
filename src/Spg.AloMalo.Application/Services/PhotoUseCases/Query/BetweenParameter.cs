using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class BetweenParameter<TProperty> : IQueryParameter where TProperty : IComparable<TProperty>
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public BetweenParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            if (operation.ToLower() == "between")
            {
                var values = value.Split(',');
                if (values.Length != 2)
                {
                    throw new ArgumentException("Between operation requires two values.");
                }

                var lowerValue = (TProperty)Convert.ChangeType(values[0], typeof(TProperty));
                var upperValue = (TProperty)Convert.ChangeType(values[1], typeof(TProperty));
                return _photoFilterBuilder.BetweenFilter(propertyExpression, lowerValue, upperValue);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
