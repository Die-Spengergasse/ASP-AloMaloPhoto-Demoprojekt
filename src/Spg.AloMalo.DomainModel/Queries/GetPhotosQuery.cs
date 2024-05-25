using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Queries
{
    public record GetPhotosQuery(
        string Filter,
        string? Order);
}
