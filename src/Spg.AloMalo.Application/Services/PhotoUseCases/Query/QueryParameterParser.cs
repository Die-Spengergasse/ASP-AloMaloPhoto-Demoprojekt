using Spg.AloMalo.DomainModel.Model;
using System;
using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public static class QueryParameterParser
    {
        public static (Expression<Func<Photo, TProperty>> propertyExpression, string operation, string value) Parse<TProperty>(string queryParameter)
        {
            var parts = queryParameter.Split(' ');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Query parameter must be in the format 'property operation value'");
            }

            var property = parts[0];
            var operation = parts[1];
            var value = parts[2];

            var parameter = Expression.Parameter(typeof(Photo), "photo");
            var propertyInfo = typeof(Photo).GetProperty(property);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{property}' not found on type 'Photo'");
            }

            var propertyExpression = Expression.Lambda<Func<Photo, TProperty>>(
                Expression.Property(parameter, propertyInfo),
                parameter
            );

            return (propertyExpression, operation, value);
        }
    }
}
