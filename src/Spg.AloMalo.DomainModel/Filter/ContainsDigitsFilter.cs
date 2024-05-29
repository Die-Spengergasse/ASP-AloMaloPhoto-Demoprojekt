using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

public class ContainsDigitsFilter<T> : IFilterOperation<T, string>
{
    public Expression<Func<T, bool>> Apply(Expression<Func<T, string>> propertySelector, string value)
    {
        var charParam = Expression.Parameter(typeof(char), "c");
        var isDigitPredicate = Expression.Lambda<Func<char, bool>>(
            Expression.Call(typeof(char).GetMethod("IsDigit", new Type[] { typeof(char) })!, charParam),
            charParam
        );

        var containsDigitExpression = Expression.Call(
            typeof(Enumerable),
            "Any",
            new[] { typeof(char) },
            propertySelector.Body,
            isDigitPredicate
        );

        return Expression.Lambda<Func<T, bool>>(containsDigitExpression, propertySelector.Parameters);
    }
}