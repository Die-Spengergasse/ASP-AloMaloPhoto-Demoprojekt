using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public record PhoneNumber(int CountryCode, int AreaCode, string SerialNumber)
    {
        // TODO: Logik zum zerteilen/zusammensetzten der Nummer
    }
}
