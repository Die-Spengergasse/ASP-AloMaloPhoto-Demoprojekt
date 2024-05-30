using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Filter
{
    public class EqualsFilter<T, TProperty> : IFilter<T>
    {
        private readonly Expression<Func<T, TProperty>> _propertyExpression;
        private readonly TProperty _value;

        public EqualsFilter(Expression<Func<T, TProperty>> propertyExpression, TProperty value)
        {
            _propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Applies the 'Equals' filter to the given query.
        /// </summary>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameter = _propertyExpression.Parameters.Single();
            var body = Expression.Equal(_propertyExpression.Body, Expression.Constant(_value));
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            return query.Where(predicate);
        }
    }
}
