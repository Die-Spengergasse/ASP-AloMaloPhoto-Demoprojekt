using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IReadOnlyPhotoRepository : IRepositoryBase<Photo>
    {
        IFilterBuilderBase<Photo, IPhotoFilterBuilder> FilterBuilder { get; }
    }
}
