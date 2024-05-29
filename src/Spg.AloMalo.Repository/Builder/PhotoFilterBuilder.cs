using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        public IQueryable<Photo> Build()
        {
            return EntityList;
        }

        public IPhotoFilterBuilder ApplyFilter<TProperty>(Expression<Func<Photo, TProperty>> propertySelector, string value, IFilterOperation<Photo, TProperty> filterOperation)
        {
            var filterExpression = filterOperation.Apply(propertySelector, value);
            EntityList = EntityList.Where(filterExpression);
            return this;
        }
    }
}