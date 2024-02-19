using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces
{
    public interface ITimeStampProvider
    {
        public DateTime Now { get; }
    }
}
