namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IEntityFilterBuilder<TEntity>
    {
        IQueryable<TEntity> EntityList { get; set; }
        IQueryable<TEntity> Build();
    }
}