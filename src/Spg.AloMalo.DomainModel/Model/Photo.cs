using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public class Photo
    {
        public PhotoId Id { get; private set; } = default!; // PK, auto increment
        [StringLength(5, ErrorMessage = "zu lang")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationTimeStamp { get; private set; }
        public ImageTypes ImageType { get; private set; }
        public Location Location { get; set; } = default!;
        public int Width { get; set; }
        public int Height { get; set; }
        public Orientations Orientation { get; set; }
        public bool AiGenerated { get; private set; }

        [JsonIgnore()]
        public Photographer? PhotographerNavigation { get; set; }

        private List<AlbumPhoto> _albumPhotos = new();
        [JsonIgnore()]
        public IReadOnlyList<AlbumPhoto> AlbumPhotos => _albumPhotos;

        private Photo()
        { }
        public Photo(
            [StringLength(5, ErrorMessage = "zu lang")] string name,
            string description,
            DateTime creationTimeStamp,
            ImageTypes imageType,
            Location location,
            int width,
            int height,
            Orientations orientation,
            bool aiGenerated,
            Photographer photographer)
        {
            Name = name;
            Description = description;
            CreationTimeStamp = creationTimeStamp;
            ImageType = imageType;
            Location = location;
            Width = width;
            Height = height;
            Orientation = orientation;
            AiGenerated = aiGenerated;
            PhotographerNavigation = photographer;
        }
        public Photo AddAlbums(IEnumerable<Album> photos)
        {
            _albumPhotos
                .AddRange(photos?
                    .Where(p => p is not null)
                    .Select(p => new AlbumPhoto(p, this, 1))
                        ?? new List<AlbumPhoto>());
            return this;
        }

        public Photo AddAlbums(params Album[] photos)
        {
            AddAlbums(photos as IEnumerable<Album>);
            return this;
        }

        public Photo AddAlbum(Album newAlbum)
        {
            if (newAlbum is not null)
            {
                _albumPhotos.Add(new AlbumPhoto(newAlbum, this, 1));
            }
            return this;
        }
    }
}
