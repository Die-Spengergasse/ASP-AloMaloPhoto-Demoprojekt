using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyHeightGreaterThanFilter(int height);
        IPhotoFilterBuilder ApplyHeightGreaterThanEqualFilter(int height);
        IPhotoFilterBuilder ApplyHeightLowerThanFilter(int height);
        IPhotoFilterBuilder ApplyHeightLowerThanEqualFilter(int height);
        IPhotoFilterBuilder ApplyHeightEqualFilter(int height);

        IPhotoFilterBuilder ApplyNameContainsFilter(string part);
        IPhotoFilterBuilder ApplyNameEqualsFilter(string part);

        IPhotoFilterBuilder ApplyNameStartsWithFilter(string part);
        IPhotoFilterBuilder ApplyNameEndsWithFilter(string part);


        IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter);
        IQueryable<Photo> Build();

    }
}
