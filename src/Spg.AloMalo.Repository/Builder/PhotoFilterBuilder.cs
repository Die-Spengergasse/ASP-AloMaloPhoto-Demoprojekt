using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System.Linq.Expressions;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }
        private readonly List<IFilter<Photo>> _filters = new();
        private readonly List<Expression<Func<Photo, bool>>> _filterExpressions = new();

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        //public IQueryable<Photo> Build()
        //{
        //    return EntityList;
        //}

        public IQueryable<Photo> Build()
        {
            var filteredPhotos = EntityList;

            foreach (var filter in _filters)
            {
                filteredPhotos = filteredPhotos.Where(photo => filter.Apply(photo)).AsQueryable();
            }

            return filteredPhotos;
        }

        public IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public IPhotoFilterBuilder ApplyExpression(Expression<Func<Photo, bool>> filterExpression)
        {
            _filterExpressions.Add(filterExpression);
            return this;
        }

        public IPhotoFilterBuilder ApplyIdFilter(PhotoId id)
        {
            EntityList = EntityList.Where(x => x.Id == id);
            return this;
        }
        public IPhotoFilterBuilder ApplyNameContainsFilter(string name)
        {
            EntityList = EntityList.Where(x => x.Name.Contains(name));
            return this;
        }
        public IPhotoFilterBuilder ApplyNameBeginsWithFilter(string name)
        {
            EntityList = EntityList.Where(x => x.Name.StartsWith(name));
            return this;
        }
        public IPhotoFilterBuilder ApplyNameEndsWithFilter(string name)
        {
            EntityList = EntityList.Where(x => x.Name.EndsWith(name));
            return this;
        }
        public IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation)
        {
            EntityList = EntityList.Where(x => x.Orientation == orientation);
            return this;
        }
        public IPhotoFilterBuilder ApplyAiFilter(bool @is)
        {
            EntityList = EntityList.Where(x => x.AiGenerated == @is);
            return this;
        }
    }
}
