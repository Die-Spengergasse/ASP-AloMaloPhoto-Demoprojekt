using Spg.AloMalo.DomainModel.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.AloMalo.DomainModel.Commands
{
    public record CreatePhotoCommand(
        [StringLength(5, ErrorMessage = "Name zu lang")] string Name, 
        string Description, 
        ImageTypes ImageType, 
        Location Location, 
        int Width, 
        int Height, 
        Orientations Orientation, 
        bool AiGenerated,
        DateTime CreationTimeStamp);
}
