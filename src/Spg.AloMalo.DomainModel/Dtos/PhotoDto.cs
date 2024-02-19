using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Dtos
{
    public class PhotoDto
    {
        public PhotoDto(string name, string description, ImageTypes imageType, Orientations orientation)
        {
            Name = name;
            Description = description;
            ImageType = imageType;
            Orientation = orientation;
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ImageTypes ImageType { get; private set; }
        public Orientations Orientation { get; set; }
    }
}
