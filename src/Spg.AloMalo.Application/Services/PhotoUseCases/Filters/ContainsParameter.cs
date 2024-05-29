using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class ContainsParameter : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public ContainsParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("ct")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<string>(queryParameter);

            ForProperty(queryParameter, propertyExpression)
                .Use<string>((expr, val) => _photoFilterBuilder.ContainsFilter(expr, val));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
