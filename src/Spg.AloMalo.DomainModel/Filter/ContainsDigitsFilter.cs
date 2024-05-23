using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.DomainModel.Filter;

public class ContainsDigitsFilter<T> : IFilterOperation<T, string>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, string>> propertySelector, string value)
    {
        return Expression.Lambda<Func<T, bool>>(
            Expression.Call(typeof(Enumerable), "Any", new[] { typeof(char) },
                propertySelector.Body, Expression.Lambda<Func<char, bool>>(
                    Expression.Call(typeof(char).GetMethod("IsDigit", new[] { typeof(char) })!, Expression.Parameter(typeof(char), "c")),
                    Expression.Parameter(typeof(char), "c"))),
            propertySelector.Parameters);
    }
}