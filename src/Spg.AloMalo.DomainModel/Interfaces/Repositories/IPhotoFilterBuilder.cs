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
        IPhotoFilterBuilder ApplyDescriptionContainsFilter(string filter);
        IPhotoFilterBuilder ApplyDescriptionBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyDescriptionEndsWithFilter(string filter);
        //IPhotoFilterBuilder ApplyPaging(int page, int size);
    }
}
