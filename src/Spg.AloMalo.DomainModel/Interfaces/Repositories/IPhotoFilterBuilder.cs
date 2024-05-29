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
        IPhotoFilterBuilder ApplyHightHigherThan(int hight);
        IPhotoFilterBuilder ApplyDescriptionContainsFilter(string desc);
        IPhotoFilterBuilder ApplyDescriptionStartsFilter(string desc);
        IPhotoFilterBuilder ApplyDescriptionEndssFilter(string desc);
        IPhotoFilterBuilder ApplyHightLowerThan(int hight);
        IPhotoFilterBuilder ApplyHightHigherOrEquals(int hight);
        IPhotoFilterBuilder ApplyHightLowerOrEquals(int hight);
        IPhotoFilterBuilder ApplyHightEquals(int hight);
        //IPhotoFilterBuilder ApplyPaging(int page, int size);
    }
}
