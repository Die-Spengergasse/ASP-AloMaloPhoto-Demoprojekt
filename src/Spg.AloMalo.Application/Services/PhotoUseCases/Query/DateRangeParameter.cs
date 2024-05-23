using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class DateRangeParameter : IQueryParameter
    {
        private readonly IFilterBuilderBase<Photo, IPhotoFilterBuilder> _photoFilterBuilder;

        public DateRangeParameter(IFilterBuilderBase<Photo, IPhotoFilterBuilder> photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var (propertyExpression, operation, value) = QueryParameterParser.Parse<DateTime>(queryParameter);
            if (operation.ToLower() == "daterange")
            {
                var values = value.Split(',');
                if (values.Length != 2)
                {
                    throw new ArgumentException("DateRange operation requires two values.");
                }

                var startDate = DateTime.Parse(values[0]);
                var endDate = DateTime.Parse(values[1]);
                return _photoFilterBuilder.DateRangeFilter(propertyExpression, startDate, endDate);
            }
            return (IPhotoFilterBuilder)_photoFilterBuilder;
        }
    }
}
