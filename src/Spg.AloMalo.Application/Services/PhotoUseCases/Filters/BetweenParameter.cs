using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class BetweenParameter<TProperty> : InterpretParameterBase<Photo>, IQueryParameter where TProperty : IComparable<TProperty>
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public BetweenParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("between")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<TProperty>(queryParameter);
            var values = value.Split(' ').Select(v => (TProperty)Convert.ChangeType(v, typeof(TProperty))).ToArray();

            ForProperty(queryParameter, propertyExpression)
                .Use<TProperty, TProperty>((expr, lower, upper) => _photoFilterBuilder.BetweenFilter(expr, lower, upper));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
