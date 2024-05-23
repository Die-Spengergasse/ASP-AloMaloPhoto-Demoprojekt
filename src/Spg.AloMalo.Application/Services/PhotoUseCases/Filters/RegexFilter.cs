using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Spg.AloMalo.DomainModel.Interfaces;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class RegexFilter<T> : IFilters<T>
{
    private readonly Expression<Func<T, bool>> _predicate;
    private readonly string _pattern;

    public RegexFilter(Expression<Func<T, bool>> predicate, string pattern)
    {
        _predicate = predicate;
        _pattern = pattern;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Where(_predicate);
    }
}