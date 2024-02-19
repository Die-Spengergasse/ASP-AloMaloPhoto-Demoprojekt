using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.IdConverters
{
    public class AlbumPhotoIdValueConverter : ValueConverter<AlbumPhotoId, int>
    {
        public AlbumPhotoIdValueConverter(ConverterMappingHints mappingHints = null!)
            : base(
                id => id.Value,
                value => new AlbumPhotoId(value),
                mappingHints
            )
        { }
    }
}
