using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.IdConverters
{
    public class AlbumIdValueConverter : ValueConverter<AlbumId, int>
    {
        public AlbumIdValueConverter(ConverterMappingHints mappingHints = null!)
            : base(
                id => id.Value,
                value => new AlbumId(value),
                mappingHints
            )
        { }
    }
}
