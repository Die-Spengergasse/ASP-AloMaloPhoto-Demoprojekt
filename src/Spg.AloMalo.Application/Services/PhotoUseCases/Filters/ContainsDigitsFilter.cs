using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Spg.AloMalo.DomainModel.Interfaces;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class ContainsDigitsFilter<T> : IFilters<T>
{
    private readonly Expression<Func<T, bool>> _predicate;

    public ContainsDigitsFilter(Expression<Func<T, bool>> predicate)
    {
        _predicate = predicate;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Where(_predicate);
    }
}
