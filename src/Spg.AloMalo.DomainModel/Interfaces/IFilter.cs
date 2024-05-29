namespace Spg.AloMalo.DomainModel.Interfaces
{
    public interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }

}
