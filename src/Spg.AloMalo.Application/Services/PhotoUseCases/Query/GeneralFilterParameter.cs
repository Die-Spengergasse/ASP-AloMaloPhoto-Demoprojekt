using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GeneralFilterParameter : IQueryParameter
    {
        public IPhotoFilterBuilder Compile(IPhotoFilterBuilder builder, string queryParameter)
        {
            var filterExpressions = ParseFiltersToExpressions(queryParameter);

            foreach (var filterExpression in filterExpressions)
            {
                builder = builder.ApplyExpression(filterExpression);
            }

            return builder;
        }

        public IEnumerable<Expression<Func<Photo, bool>>> ParseFiltersToExpressions(string filterQuery)
        {
            var expressions = new List<Expression<Func<Photo, bool>>>();

            if (string.IsNullOrWhiteSpace(filterQuery))
                return expressions;

            var parts = filterQuery.Split(' ');

            for (int i = 0; i < parts.Length; i += 3)
            {
                if (i + 2 >= parts.Length)
                {
                    // Ensure there are enough parts to avoid out-of-range errors
                    continue;
                }

                var property = parts[i]?.Trim();
                var operation = parts[i + 1]?.Trim().ToLower();
                var value = parts[i + 2]?.Trim();

                if (string.IsNullOrEmpty(property) || string.IsNullOrEmpty(operation) || string.IsNullOrEmpty(value))
                {
                    // Skip if any of the required parts are null or empty
                    continue;
                }

                var parameter = Expression.Parameter(typeof(Photo), "p");
                var member = Expression.Property(parameter, property);
                var constant = Expression.Constant(value);

                Expression body = operation switch
                {
                    "eq" => Expression.Equal(member, constant),
                    "ct" => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                    "bw" => Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant),
                    "ew" => Expression.Call(member, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant),
                    _ => throw new ArgumentException($"Unsupported filter operation: {operation}")
                };

                var lambda = Expression.Lambda<Func<Photo, bool>>(body, parameter);
                expressions.Add(lambda);
            }

            return expressions;
        }
    }
}
