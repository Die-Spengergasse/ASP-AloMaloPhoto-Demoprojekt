using Spg.AloMalo.DomainModel.Interfaces;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.DomainModel.Filters
{
    public class RegexFilter<T> : IFilter<T>
    {
        private readonly Expression<Func<T, string>> _propertyExpression;
        private readonly string _pattern;

        public RegexFilter(Expression<Func<T, string>> propertyExpression, string pattern)
        {
            _propertyExpression = propertyExpression;
            _pattern = pattern;
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var parameter = _propertyExpression.Parameters.Single();
            var body = Expression.Call(
                typeof(Regex).GetMethod(nameof(Regex.IsMatch), new[] { typeof(string), typeof(string) }),
                _propertyExpression.Body,
                Expression.Constant(_pattern));
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
            return query.Where(predicate);
        }
    }
}
