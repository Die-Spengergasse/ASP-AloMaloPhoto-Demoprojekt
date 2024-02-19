using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public record EMail(string Address)
    {
        //public int Id { get; set; } // V.O. hat niemals PK
    }
}
