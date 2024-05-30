using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Interfaces;


namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        private readonly List<IFilter<Photo>> _filters = new();

        public IQueryable<Photo> EntityList { get; set; }

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        

        public IPhotoFilterBuilder ApplyHeightGreaterThanFilter(int height)
        {
            EntityList = EntityList.Where(x => x.Height > height);
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightGreaterThanEqualFilter(int height)
        {
            EntityList = EntityList.Where(x => x.Height >= height);
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightLowerThanFilter(int height)
        {
            EntityList = EntityList.Where(x => x.Height < height);
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightLowerThanEqualFilter(int height)
        {
            EntityList = EntityList.Where(x => x.Height <= height);
            return this;
        }

        public IPhotoFilterBuilder ApplyNameContainsFilter(string part)
        {
            EntityList = EntityList.Where(x => x.Name.Trim().ToLower().Contains(part.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyNameStartsWithFilter(string part)
        {
            EntityList = EntityList.Where(x => x.Name.Trim().ToLower().StartsWith(part.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyFilter(IFilter<Photo> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public IQueryable<Photo> Build()
        {
            var query = EntityList;
            foreach (var filter in _filters)
            {
                query = filter.Apply(query);
            }
            return query;
        }
    }

}