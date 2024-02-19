using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.IdConverters
{
    public class PersonIdValueConverter : ValueConverter<PersonId, int>
    {
        public PersonIdValueConverter(ConverterMappingHints mappingHints = null!)
            : base(
                id => id.Value,
                value => new PersonId(value),
                mappingHints
            )
        { }
    }
}
