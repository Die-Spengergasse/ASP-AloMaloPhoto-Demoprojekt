using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Iso2Code { get; set; } = string.Empty;
        public int ConuntryCode { get; set; }
        public string TopLevelDomain { get; set; } = string.Empty;

        protected Country()
        { }
        public Country(
            string name,
            string iso2Code,
            int conuntryCode,
            string topLevelDomain)
        {
            Name = name;
            Iso2Code = iso2Code;
            ConuntryCode = conuntryCode;
            TopLevelDomain = topLevelDomain;
        }
    }
}
