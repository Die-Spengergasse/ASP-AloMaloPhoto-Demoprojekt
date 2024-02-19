using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public record Address(string StreetNumber, string ZipCode, string City, string Country)
    {
        public State State { get; set; }
        // TODO: Logik ... lassen wir uns noch einfallen
    }

    public class State
    {
        public string Name { get; set; }
    }
}
