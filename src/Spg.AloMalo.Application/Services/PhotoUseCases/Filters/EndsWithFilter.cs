using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class EndsWithFilter<T> : IFilters<T>
{
    private readonly Expression<Func<T, bool>> _predicate;
    private readonly string _value;

    public EndsWithFilter(Expression<Func<T, bool>> predicate, string value)
    {
        _predicate = predicate;
        _value = value;
    }

    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Where(_predicate);
    }
}