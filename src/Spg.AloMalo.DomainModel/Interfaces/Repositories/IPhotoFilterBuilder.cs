using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IQueryable<Photo> EntityList { get; set; }
        IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter);
        IQueryable<Photo> Build();
        IPhotoFilterBuilder ApplyWidthGreaterThanFilter(int start);
        IPhotoFilterBuilder ApplyWidthGreaterThanEqualFilter(int start);
        IPhotoFilterBuilder ApplyWidthLowerThanFilter(int start);
        IPhotoFilterBuilder ApplyWidthLowerThanEqualFilter(int start);
        IPhotoFilterBuilder ApplyNameContainsFilter(string namePart);
        IPhotoFilterBuilder ApplyDescriptionContainsFilter(string namePart);
        IPhotoFilterBuilder ApplyNameStartsWithFilter(string namePart);
        IPhotoFilterBuilder ApplyDescriptionStartsWithFilter(string namePart);
        IPhotoFilterBuilder ApplyCreationTimeStampBetweenFilter(DateTime start, DateTime end);
    }
}
