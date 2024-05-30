using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);
        IPhotoFilterBuilder ApplyContainsFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyBeginsWithFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyEndsWithFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplyAiFilter(bool @is);
        IPhotoFilterBuilder ApplyEqualsFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyContainsDigitsFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyGreaterthanFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyGreaterthanequalFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyItFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplyIteFilter(string filter, string propertyName);
        IPhotoFilterBuilder ApplycontainsspecialcharsFilter(string filter, string propertyName);

        //IPhotoFilterBuilder ApplyPaging(int page, int size);
    }
}
