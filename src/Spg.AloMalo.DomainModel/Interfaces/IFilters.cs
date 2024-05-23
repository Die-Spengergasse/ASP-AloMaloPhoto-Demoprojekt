namespace Spg.AloMalo.DomainModel.Interfaces;

public interface IFilters<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}