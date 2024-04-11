using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Dtos
{
    public record AlbumDto(
        [StringLength(maximumLength: 5)]
        string Name,
        string Description,
        DateTime CreationTimeStamp
    );
}
