using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Model.RichTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Infrastructure.RichTypesConverters
{
    public class PersonFirstNameConverter : ValueConverter<FirstName, string>
    {
        public PersonFirstNameConverter()
            : base(
                v => v.Value,
                v => new FirstName(v)
            )
        { }
    }
}
