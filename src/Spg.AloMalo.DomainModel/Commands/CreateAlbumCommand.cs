using Spg.AloMalo.DomainModel.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Commands
{
    public class CreateAlbumCommand
    {
        [StringLength(maximumLength: 7, ErrorMessage ="zu lange")]
        [NoSpecialFirstNameAttribute("Homer")]
        public string Name { get; set; }
        public string Description { get; set; }
        // TODO: Implementaton
    }
}
