using Spg.AloMalo.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Filter
{
    public class EndsWithFilter<T> : IFilter<T>
    {
        private readonly Expression<Func<T, string>> _propertyExpression;
        private readonly string _value;

        public EndsWithFilter(Expression<Func<T, string>> propertyExpression, string value)
        {
            _propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Applies the 'EndsWith' filter to the given query.
        /// </summary>
        /// <param name="query">The query to filter.</param>
        /// <returns>The filtered query.</returns>
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parameter = _propertyExpression.Parameters.Single();
            var body = Expression.Call(_propertyExpression.Body, nameof(string.EndsWith), Type.EmptyTypes, Expression.Constant(_value));
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            return query.Where(predicate);
        }
    }
}
