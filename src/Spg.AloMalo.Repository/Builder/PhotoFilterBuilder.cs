using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Repository.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }

        private readonly List<Expression<Func<Photo, bool>>> _conditions = new List<Expression<Func<Photo, bool>>>();

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        public IQueryable<Photo> Build()
        {
            var query = EntityList;
            foreach (var condition in _conditions)
            {
                query = query.Where(condition);
            }
            return query;
        }

        public IPhotoFilterBuilder ApplyEqualsFilter(string propertyName, string value)
        {
            _conditions.Add(BuildStringComparisonExpression(propertyName, value, nameof(string.Equals)));
            return this;
        }

        public IPhotoFilterBuilder ApplyContainsFilter(string propertyName, string value)
        {
            _conditions.Add(BuildStringComparisonExpression(propertyName, value, nameof(string.Contains)));
            return this;
        }

        public IPhotoFilterBuilder ApplyStartsWithFilter(string propertyName, string value)
        {
            _conditions.Add(BuildStringComparisonExpression(propertyName, value, nameof(string.StartsWith)));
            return this;
        }

        public IPhotoFilterBuilder ApplyEndsWithFilter(string propertyName, string value)
        {
            _conditions.Add(BuildStringComparisonExpression(propertyName, value, nameof(string.EndsWith)));
            return this;
        }

        public IPhotoFilterBuilder ApplyGreaterThanFilter(string propertyName, string value)
        {
            _conditions.Add(BuildNumericComparisonExpression(propertyName, value, ExpressionType.GreaterThan));
            return this;
        }

        public IPhotoFilterBuilder ApplyGreaterThanOrEqualFilter(string propertyName, string value)
        {
            _conditions.Add(BuildNumericComparisonExpression(propertyName, value, ExpressionType.GreaterThanOrEqual));
            return this;
        }

        public IPhotoFilterBuilder ApplyLessThanFilter(string propertyName, string value)
        {
            _conditions.Add(BuildNumericComparisonExpression(propertyName, value, ExpressionType.LessThan));
            return this;
        }

        public IPhotoFilterBuilder ApplyLessThanOrEqualFilter(string propertyName, string value)
        {
            _conditions.Add(BuildNumericComparisonExpression(propertyName, value, ExpressionType.LessThanOrEqual));
            return this;
        }

        private Expression<Func<Photo, bool>> BuildStringComparisonExpression(string propertyName, string value, string methodName)
        {
            var paramExpr = Expression.Parameter(typeof(Photo), "photo");
            var propertyExpr = Expression.Property(paramExpr, propertyName);
            var valueExpr = Expression.Constant(value, typeof(string));
            var comparisonMethod = typeof(string).GetMethod(methodName, new[] { typeof(string), typeof(StringComparison) });
            var methodCallExpr = Expression.Call(propertyExpr, comparisonMethod, valueExpr, Expression.Constant(StringComparison.OrdinalIgnoreCase));

            return Expression.Lambda<Func<Photo, bool>>(methodCallExpr, paramExpr);
        }

        private Expression<Func<Photo, bool>> BuildNumericComparisonExpression(string propertyName, string value, ExpressionType comparisonType)
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                var paramExpr = Expression.Parameter(typeof(Photo), "photo");
                var propertyExpr = Expression.Property(paramExpr, propertyName);
                var decimalExpr = Expression.Constant(decimalValue);
                var convertExpr = Expression.Convert(propertyExpr, typeof(decimal?));
                var comparisonExpr = Expression.MakeBinary(comparisonType, convertExpr, decimalExpr);

                return Expression.Lambda<Func<Photo, bool>>(comparisonExpr, paramExpr);
            }
            throw new ArgumentException("Value provided is not a valid decimal.", nameof(value));
        }
    }
}
