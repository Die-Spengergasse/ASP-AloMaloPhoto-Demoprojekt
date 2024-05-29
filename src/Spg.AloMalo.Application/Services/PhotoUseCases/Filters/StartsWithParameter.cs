using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class StartsWithParameter : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public StartsWithParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("sw")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<string>(queryParameter);

            ForProperty(queryParameter, propertyExpression)
                .Use<string>((expr, val) => _photoFilterBuilder.StartsWithFilter(expr, val));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
