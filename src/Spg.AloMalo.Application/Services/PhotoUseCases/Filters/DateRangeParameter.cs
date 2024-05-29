using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class DateRangeParameter : InterpretParameterBase<Photo>, IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public DateRangeParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
            : base("dr")
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<DateTime>(queryParameter);
            var values = value.Split(' ').Select(DateTime.Parse).ToArray();

            ForProperty(queryParameter, propertyExpression)
                .Use<DateTime, DateTime>((expr, start, end) => _photoFilterBuilder.DateRangeFilter(expr, start, end));

            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
