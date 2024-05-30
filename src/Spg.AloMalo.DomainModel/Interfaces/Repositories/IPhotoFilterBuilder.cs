using Spg.AloMalo.DomainModel.Model;
using System.Linq;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter);
        new IQueryable<Photo> Build();
    }
}
