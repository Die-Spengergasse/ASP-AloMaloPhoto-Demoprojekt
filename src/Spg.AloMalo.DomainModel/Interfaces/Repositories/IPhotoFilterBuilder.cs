using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);
        IPhotoFilterBuilder ApplyNameContainsFilter(string filter);
        IPhotoFilterBuilder ApplyNameBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyNameEndsWithFilter(string filter);
        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplyAiFilter(bool @is);
        //IPhotoFilterBuilder ApplyPaging(int page, int size);
        IPhotoFilterBuilder ApplyDescriptionBeginsWithFilter(string description);
        IPhotoFilterBuilder ApplyDescriptionContainsFilter(string description);
        IPhotoFilterBuilder ApplyDescriptionEndsWithFilter(string description);
        IPhotoFilterBuilder ApplyDescriptionEqualsFilter(string description);
        IPhotoFilterBuilder ApplyNameEqualsFilter(string name);
        IPhotoFilterBuilder ApplyHeightGreaterThanFilter(string filter);
        IPhotoFilterBuilder ApplyHeightLowerThanFilter(string filter);
        IPhotoFilterBuilder ApplyHeightEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyHeightPlusMinus100Filter(string filter);
    }
}
