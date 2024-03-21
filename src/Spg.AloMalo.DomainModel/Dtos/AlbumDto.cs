using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Dtos
{
    public class AlbumDto
    {
        [StringLength(maximumLength: 5)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationTimeStamp { get; private set; }

        //public Photographer Owner { get; set; } = default!;
    }
}
