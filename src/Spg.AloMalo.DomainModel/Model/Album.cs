using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Validators;

namespace Spg.AloMalo.DomainModel.Model
{
    public class Album : EntityBase<Album>, IValidateableEntity<Album>
    {
        public AlbumId Id { get; set; } = default!;
        
        [NoSpecialFirstName("Homer")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public DateTime CreationTimeStamp  { get; private set; }
        public bool Private { get; set; }
        
        public Photographer Owner { get; set; } = default!;

        private List<AlbumPhoto> _albumPhotos = new();
        public IReadOnlyList<AlbumPhoto> AlbumPhotos => _albumPhotos;

        protected Album()
        { }
        public Album(
            [NoSpecialFirstNameAttribute("Homer")] string name, 
            string description, 
            bool @private,
            Photographer owner,
            ITimeStampProvider timeStampProvider)
        {
            Name = name;
            Description = description;
            Private = @private;
            Owner = owner;

            CreationTimeStamp = timeStampProvider.Now;
        }

        public Album AddPhotos(IEnumerable<Photo> photos)
        {
            _albumPhotos
                .AddRange(photos?
                    .Where(p => p is not null)
                    .Select(p => new AlbumPhoto(this, p, 1)) 
                        ?? new List<AlbumPhoto>());
            return this;
        }

        public Album AddPhotos(params Photo [] photos)
        {
            AddPhotos(photos as IEnumerable<Photo>);
            return this;
        }

        public Album AddPhoto(Photo newPhoto)
        {
            if (newPhoto is not null)
            {
                _albumPhotos.Add(new AlbumPhoto(this, newPhoto, 1));
            }
            return this;
        }

        public Album Validate()
        {
            Validate(new() 
            {
                CheckName,
                () => (Description.ToString().ToLower().Contains("simpson".ToLower()), "Description enthält Simpson")
            });
            return this;
        }

        private (bool, string) CheckName()
        {
            return (Name.ToString().ToLower().Contains("HOMER".ToLower()), "Name enthält Homer");
        }
    }
}
