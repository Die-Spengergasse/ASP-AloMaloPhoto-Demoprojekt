using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.IdConverters
{
    public class PhotographerIdValueConverter : ValueConverter<PhotographerId, int>
    {
        public PhotographerIdValueConverter(ConverterMappingHints mappingHints = null!)
            : base(
                id => id.Value,
                value => new PhotographerId(value),
                mappingHints
            )
        { }
    }
}
