using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);

        //Contains
        //Name
        IPhotoFilterBuilder ApplyNameEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyNameContainsFilter(string filter);
        IPhotoFilterBuilder ApplyNameBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyNameEndsWithFilter(string filter);

        //Description
        IPhotoFilterBuilder ApplyDescriptionEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyDescriptionContainsFilter(string filter);
        IPhotoFilterBuilder ApplyDescriptionBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyDescriptionEndsWithFilter(string filter);
        //Date
        IPhotoFilterBuilder ApplyDateContainsFilter(string filter);

        //Width
        IPhotoFilterBuilder ApplyWidthLessThanFilter(string filter);
        IPhotoFilterBuilder ApplyWidthLessThanEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyWidthEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyWidthGreaterThanEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyWidthGreaterThanFilter(string filter);

        //Height
        IPhotoFilterBuilder ApplyHeightLessThanFilter(string filter);
        IPhotoFilterBuilder ApplyHeightLessThanEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyHeightEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyHeightGreaterThanEqualsFilter(string filter);
        IPhotoFilterBuilder ApplyHeightGreaterThanFilter(string filter);



        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplyAiFilter(bool @is);
        //IPhotoFilterBuilder ApplyPaging(int page, int size);
    }
}
