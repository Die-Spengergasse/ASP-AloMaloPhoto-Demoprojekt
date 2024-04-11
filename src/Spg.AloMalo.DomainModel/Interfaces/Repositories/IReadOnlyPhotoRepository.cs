using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IReadOnlyPhotoRepository : IRepositoryBase<Photo>
    {
        IPhotoFilterBuilder FilterBuilder { get; }
    }
}
