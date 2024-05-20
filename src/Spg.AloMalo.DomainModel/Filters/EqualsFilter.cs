using Spg.AloMalo.DomainModel.Interfaces;
using System.Linq.Expressions;

namespace Spg.AloMalo.DomainModel.Filters
{
    public class EqualsFilter<T, TProperty> : IFilter<T>
    {
        private readonly Expression<Func<T, TProperty>> _propertyExpression;
        private readonly TProperty _value;

        public EqualsFilter(Expression<Func<T, TProperty>> propertyExpression, TProperty value)
        {
            _propertyExpression = propertyExpression;
            _value = value;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            return query.Where(Expression.Lambda<Func<T, bool>>(
                Expression.Equal(_propertyExpression.Body, Expression.Constant(_value)), _propertyExpression.Parameters));
        }
    }
}
