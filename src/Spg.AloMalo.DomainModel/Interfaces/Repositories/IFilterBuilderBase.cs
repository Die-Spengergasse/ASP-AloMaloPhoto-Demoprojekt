using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IFilterBuilderBase<TEntity, TFilterBuilder>
            where TEntity : class
            where TFilterBuilder : class
    {
        TFilterBuilder ApplyFilter(Expression<Func<TEntity, bool>> filterExpression);
        TFilterBuilder EqualsFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        TFilterBuilder NotEqualsFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        TFilterBuilder StartsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder NotStartsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder EndsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder NotEndsWithFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder ContainsFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder NotContainsFilter(Expression<Func<TEntity, string>> propertyExpression, string value);
        TFilterBuilder RegexFilter(Expression<Func<TEntity, string>> propertyExpression, string pattern);
        TFilterBuilder ContainsDigitsFilter(Expression<Func<TEntity, string>> propertyExpression);
        TFilterBuilder GreaterThanFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value) where TProperty : IComparable<TProperty>;
        TFilterBuilder LessThanFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value) where TProperty : IComparable<TProperty>;
        TFilterBuilder InFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<TProperty> values);
        TFilterBuilder NotInFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<TProperty> values);
        TFilterBuilder BetweenFilter<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty lowerValue, TProperty upperValue) where TProperty : IComparable<TProperty>;
        TFilterBuilder DateRangeFilter(Expression<Func<TEntity, DateTime>> propertyExpression, DateTime startDate, DateTime endDate);
        IQueryable<TEntity> Build();
    }
}
