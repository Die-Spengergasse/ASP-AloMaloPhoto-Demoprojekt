using Spg.AloMalo.DomainModel.Model;
using System.Linq.Expressions;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IPhotoFilterBuilder : IEntityFilterBuilder<Photo>
    {
        IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter);
        IPhotoFilterBuilder ApplyIdFilter(PhotoId id);
        IPhotoFilterBuilder ApplyNameContainsFilter(string filter);
        IPhotoFilterBuilder ApplyNameBeginsWithFilter(string filter);
        IPhotoFilterBuilder ApplyNameEndsWithFilter(string filter);
        IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation);
        IPhotoFilterBuilder ApplyAiFilter(bool @is);
        IPhotoFilterBuilder ApplyExpression(Expression<Func<Photo, bool>> filterExpression);
        //IPhotoFilterBuilder ApplyPaging(int page, int size);
    }
}
