using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        private readonly List<IFilterOperation<Photo>> _filters = new();

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        public IQueryable<Photo> EntityList { get; set; }

        public IPhotoFilterBuilder ApplyIdFilter(PhotoId id)
        {
            _filters.Add(new EqualsFilter<Photo>("Id", id.ToString()));
            return this;
        }

        public IPhotoFilterBuilder ApplyNameContainsFilter(string name)
        {
            _filters.Add(new ContainsFilter<Photo>("Name", name));
            return this;
        }

        public IPhotoFilterBuilder ApplyNameBeginsWithFilter(string name)
        {
            _filters.Add(new StartsWithFilter<Photo>("Name", name));
            return this;
        }

        public IPhotoFilterBuilder ApplyNameEndsWithFilter(string name)
        {
            _filters.Add(new EndsWithFilter<Photo>("Name", name));
            return this;
        }

        public IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation)
        {
            _filters.Add(new EqualsFilter<Photo>("Orientation", orientation.ToString()));
            return this;
        }

        public IPhotoFilterBuilder ApplyAiFilter(bool isAiGenerated)
        {
            _filters.Add(new EqualsFilter<Photo>("AiGenerated", isAiGenerated.ToString()));
            return this;
        }

        public IQueryable<Photo> Build()
        {
            return _filters.Aggregate(EntityList, (current, filter) => filter.Apply(current));
        }
    }
}
