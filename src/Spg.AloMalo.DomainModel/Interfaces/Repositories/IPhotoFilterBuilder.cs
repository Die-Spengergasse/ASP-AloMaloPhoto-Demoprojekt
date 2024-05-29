using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IQueryable<Photo> EntityList { get; set; }
        IPhotoFilterBuilder ApplyFilter(IFilters<Photo> filter);
        IQueryable<Photo> Build();
    }
}
