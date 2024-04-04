using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.AloMalo.DomainModel.Commands
{
    public record CreatePhotoCommand(
        [StringLength(5, ErrorMessage = "Name zu lang")] string Name, 
        string Description, 
        ImageTypesDto ImageType,
        LocationDto Location, 
        int Width, 
        int Height,
        bool AiGenerated,
        Guid PhotographerId);
}
