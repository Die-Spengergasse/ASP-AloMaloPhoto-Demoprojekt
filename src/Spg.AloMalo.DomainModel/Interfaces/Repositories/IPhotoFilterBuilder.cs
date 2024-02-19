using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityReaderBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);
        IPhotoFilterBuilder ApplyNameContainsFilter(string filter);
        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplayAiFilter(bool @is);
    }
}
