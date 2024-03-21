using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.RichTypesConverters
{
    public class PersonLastNameConverter : ValueConverter<LastName, string>
    {
        public PersonLastNameConverter(ConverterMappingHints mappingHints = null!)
            : base(
                lastName => lastName.Value,
                value => new LastName(value),
                mappingHints
            )
        { }
    }
}
