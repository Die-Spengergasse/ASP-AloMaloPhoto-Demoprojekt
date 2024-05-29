using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class RegexParameter : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public RegexParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("rx")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<string>(queryParameter);

            ForProperty(queryParameter, propertyExpression)
                .Use<string>((expr, val) => _photoFilterBuilder.RegexFilter(expr, val));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
