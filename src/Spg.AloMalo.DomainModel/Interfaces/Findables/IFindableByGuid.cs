using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Findables
{
    public interface IFindableByGuid
    {
        public Guid Guid{ get; }
    }
}
