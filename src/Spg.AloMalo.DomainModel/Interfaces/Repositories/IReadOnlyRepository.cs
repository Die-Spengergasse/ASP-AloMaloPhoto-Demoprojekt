using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IReadOnlyRepository<TReadBilder>
    {
        TReadBilder FilterBuilder { get; set; }
    }
}
