using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

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

        public IPhotoFilterBuilder ApplyWidthGreaterThanFilter(int start)
        {
            EntityList = EntityList.Where(x => x.Width > start);
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthGreaterThanEqualFilter(int start)
        {
            EntityList = EntityList.Where(x => x.Width >= start);
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthLowerThanFilter(int start)
        {
            EntityList = EntityList.Where(x => x.Width < start);
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthLowerThanEqualFilter(int start)
        {
            EntityList = EntityList.Where(x => x.Width <= start);
            return this;
        }

        public IPhotoFilterBuilder ApplyNameContainsFilter(string namePart)
        {
            EntityList = EntityList.Where(x => x.Name.Trim().ToLower()
                            .Contains(namePart.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyDescriptionContainsFilter(string namePart)
        {
            EntityList = EntityList.Where(x => x.Description.Trim().ToLower()
                            .Contains(namePart.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyNameStartsWithFilter(string namePart)
        {
            EntityList = EntityList.Where(x => x.Name.Trim().ToLower()
                .StartsWith(namePart.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyDescriptionStartsWithFilter(string namePart)
        {
            EntityList = EntityList.Where(x => x.Description.Trim().ToLower()
                .StartsWith(namePart.Trim().ToLower()));
            return this;
        }

        public IPhotoFilterBuilder ApplyCreationTimeStampBetweenFilter(DateTime start, DateTime end)
        {
            EntityList = EntityList.Where(x => x.CreationTimeStamp > start && x.CreationTimeStamp < end);
            return this;
        }
    }

}
