using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using System.Linq.Expressions;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }
        private readonly List<Expression<Func<Photo, bool>>> _filterExpressions = new();
        private PhotoContext? _context;

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        public IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter)
        {
            _filterExpressions.Add(filter.Apply());
            return this;
        }

        public IQueryable<Photo> Build()
        {
            var filteredPhotos = EntityList;

            foreach (var filterExpression in _filterExpressions)
            {
                filteredPhotos = filteredPhotos.Where(filterExpression);
            }

            return filteredPhotos;
        }

        //public IQueryable<Photo> Build()
        //{
        //    return EntityList;
        //}

        

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
