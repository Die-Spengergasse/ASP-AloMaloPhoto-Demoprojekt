using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyHeightGreaterThanFilter(int height);
        IPhotoFilterBuilder ApplyHeightGreaterThanEqualFilter(int height);
        IPhotoFilterBuilder ApplyHeightLowerThanFilter(int height);
        IPhotoFilterBuilder ApplyHeightLowerThanEqualFilter(int height);
        IPhotoFilterBuilder ApplyNameContainsFilter(string part);
        IPhotoFilterBuilder ApplyNameStartsWithFilter(string part);
    }
}
