using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public class AlbumPhoto
    {
        public AlbumPhotoId Id { get; set; } = default!;
        public Album AlbumNavigation { get; private set; } = default!;
        public Photo PhotoNavigation { get; private set; } = default!;
        public int Position { get; set; }

        protected AlbumPhoto()
        { }
        public AlbumPhoto(Album albumNavigation, Photo photoNavigation, int position)
        {
            AlbumNavigation = albumNavigation;
            PhotoNavigation = photoNavigation;
            Position = position;
        }
    }
}
