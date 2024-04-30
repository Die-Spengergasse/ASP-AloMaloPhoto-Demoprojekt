using System.ComponentModel.DataAnnotations;

namespace Spg.AloMalo.DomainModel.Dtos
{
    public record AlbumDto(
        [StringLength(maximumLength: 5)]
        string Name,
        string Description,
        DateTime CreationTimeStamp
    );
}
