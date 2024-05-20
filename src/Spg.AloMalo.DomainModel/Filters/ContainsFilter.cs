using Spg.AloMalo.DomainModel.Interfaces;
using System.Linq.Expressions;

namespace Spg.AloMalo.DomainModel.Filters
{
    public class ContainsFilter<T> : IFilter<T>
    {
        private readonly Expression<Func<T, string>> _propertyExpression;
        private readonly string _value;

        public ContainsFilter(Expression<Func<T, string>> propertyExpression, string value)
        {
            _propertyExpression = propertyExpression;
            _value = value;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            return query.Where(Expression.Lambda<Func<T, bool>>(
                Expression.Call(_propertyExpression.Body, nameof(string.Contains), Type.EmptyTypes, Expression.Constant(_value)),
                _propertyExpression.Parameters));
        }
    }

}
