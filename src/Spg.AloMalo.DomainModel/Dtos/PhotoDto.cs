using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Dtos
{
    public record PhotoDto(
        Guid Id,
        string Name, 
        string Description, 
        ImageTypesDto ImageType, 
        OrientationsDto Orientation);
}
