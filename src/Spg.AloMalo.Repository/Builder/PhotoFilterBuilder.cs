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

        public IPhotoFilterBuilder ApplyIdFilter(PhotoId id)
        {
            EntityList = EntityList.Where(x => x.Id == id);
            return this;
        }

        //Name
        public IPhotoFilterBuilder ApplyNameEqualsFilter(string name)
        {
            EntityList = EntityList.Where(x => x.Name.Equals(name));
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

        //Ai Filter
        public IPhotoFilterBuilder ApplyAiFilter(bool @is)
        {
            EntityList = EntityList.Where(x => x.AiGenerated == @is);
            return this;
        }
        //Description
        public IPhotoFilterBuilder ApplyDescriptionEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Description.Equals(filter));
            return this;
        }
        public IPhotoFilterBuilder ApplyDescriptionContainsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Description.Contains(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyDescriptionBeginsWithFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Description.StartsWith(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyDescriptionEndsWithFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Description.EndsWith(filter));
            return this;
        }

        //Date
        public IPhotoFilterBuilder ApplyDateContainsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.CreationTimeStamp.ToString().Contains(filter));
            return this;
        }

        //ImageType
        public IPhotoFilterBuilder ApplyImageTypeEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.ImageType.Equals(filter));
            return this;
        }

        //Width
        public IPhotoFilterBuilder ApplyWidthLessThanFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Width < int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthLessThanEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Width <= int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Width == int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthGreaterThanEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Width >= int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyWidthGreaterThanFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Width > int.Parse(filter));
            return this;
        }

        //Height
        public IPhotoFilterBuilder ApplyHeightLessThanFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Height < int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightLessThanEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Height <= int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Height == int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightGreaterThanEqualsFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Height >= int.Parse(filter));
            return this;
        }

        public IPhotoFilterBuilder ApplyHeightGreaterThanFilter(string filter)
        {
            EntityList = EntityList.Where(x => x.Height > int.Parse(filter));
            return this;
        }

        //Orientation
        public IPhotoFilterBuilder ApplyOrientationFilter(Orientations orientation)
        {
            EntityList = EntityList.Where(x => x.Orientation == orientation);
            return this;
        }
    }
}
