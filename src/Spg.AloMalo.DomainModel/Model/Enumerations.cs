using Spg.AloMalo.DomainModel.Dtos;

namespace Spg.AloMalo.DomainModel.Model
{
    public enum ImageTypes { Jpg = 0, Png, Nef, Unknown = 99 }

    public class ImageTypesMapper()
    {
        public static ImageTypesDto ToDto(ImageTypes imageType)
        {
            switch (imageType)
            {
                case ImageTypes.Nef:
                    return ImageTypesDto.Nef;
                case ImageTypes.Jpg:
                    return ImageTypesDto.Jpg;
                case ImageTypes.Png:
                    return ImageTypesDto.Png;
                default:
                    return ImageTypesDto.Unknown;
            }
        }
    }

    public enum Orientations { Portrait, Landscape, Square }

    public class OrientationsMapper()
    {
        public static OrientationsDto ToDto(Orientations orientation)
        {
            switch (orientation)
            {
                case Orientations.Portrait:
                    return OrientationsDto.Portrait;
                case Orientations.Square:
                    return OrientationsDto.Square;
                default:
                    return OrientationsDto.Landscape;
            }
        }
    }
}