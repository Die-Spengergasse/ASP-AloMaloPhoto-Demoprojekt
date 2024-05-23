using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class RegexFilter<T> : IFilterOperation<T, string>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, string>> propertySelector, string value)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.Call(typeof(Regex).GetMethod("IsMatch", new[] { typeof(string), typeof(string) })!, propertySelector.Body, Expression.Constant(value)),
            propertySelector.Parameters);
    }
}
