using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.Repository.Builder
{
    public class FilterBuilderBase<TEntity, TFilterBuilder> : IFilterBuilderBase<TEntity, TFilterBuilder>
         where TEntity : class
         where TFilterBuilder : class, IEntityFilterBuilder<TEntity>
    {
        private readonly TFilterBuilder _filterBuilder;

        public FilterBuilderBase(TFilterBuilder filterBuilder)
        {
            _filterBuilder = filterBuilder;
        }

        public TFilterBuilder ApplyFilter(Expression<Func<TEntity, bool>> filterExpression)
        {
            _filterBuilder.EntityList = _filterBuilder.EntityList.Where(filterExpression);
            return _filterBuilder;
        }

        public TFilterBuilder EqualsFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).Equals(value));
        }

        public TFilterBuilder NotEqualsFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        {
            return ApplyFilter(entity => !propertyExpression.Compile()(entity).Equals(value));
        }

        public TFilterBuilder StartsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).StartsWith(value));
        }

        public TFilterBuilder NotStartsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => !propertyExpression.Compile()(entity).StartsWith(value));
        }

        public TFilterBuilder EndsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).EndsWith(value));
        }

        public TFilterBuilder NotEndsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => !propertyExpression.Compile()(entity).EndsWith(value));
        }

        public TFilterBuilder ContainsFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).Contains(value));
        }

        public TFilterBuilder NotContainsFilter(Expression<Func<TEntity, string>> propertyExpression, string value)
        {
            return ApplyFilter(entity => !propertyExpression.Compile()(entity).Contains(value));
        }

        public TFilterBuilder RegexFilter(Expression<Func<TEntity, string>> propertyExpression, string pattern)
        {
            return ApplyFilter(entity => Regex.IsMatch(propertyExpression.Compile()(entity), pattern));
        }

        public TFilterBuilder ContainsDigitsFilter(Expression<Func<TEntity, string>> propertyExpression)
        {
            return RegexFilter(propertyExpression, @"\d");
        }

        public TFilterBuilder GreaterThanFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).CompareTo(value) > 0);
        }

        public TFilterBuilder LessThanFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
            where TProperty : IComparable<TProperty>
        {
            return ApplyFilter(entity => propertyExpression.Compile()(entity).CompareTo(value) < 0);
        }

        public TFilterBuilder InFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<TProperty> values)
        {
            var valueSet = new HashSet<TProperty>(values);
            return ApplyFilter(entity => valueSet.Contains(propertyExpression.Compile()(entity)));
        }

        public TFilterBuilder NotInFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<TProperty> values)
        {
            var valueSet = new HashSet<TProperty>(values);
            return ApplyFilter(entity => !valueSet.Contains(propertyExpression.Compile()(entity)));
        }

        public TFilterBuilder BetweenFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty lowerValue, TProperty upperValue)
            where TProperty : IComparable<TProperty>
        {
            return ApplyFilter(entity =>
                propertyExpression.Compile()(entity).CompareTo(lowerValue) >= 0 &&
                propertyExpression.Compile()(entity).CompareTo(upperValue) <= 0);
        }

        public TFilterBuilder DateRangeFilter(Expression<Func<TEntity, DateTime>> propertyExpression, DateTime startDate, DateTime endDate)
        {
            return ApplyFilter(entity =>
                propertyExpression.Compile()(entity) >= startDate &&
                propertyExpression.Compile()(entity) <= endDate);
        }

        public IQueryable<TEntity> Build()
        {
            return _filterBuilder.EntityList;
        }
    }
}
