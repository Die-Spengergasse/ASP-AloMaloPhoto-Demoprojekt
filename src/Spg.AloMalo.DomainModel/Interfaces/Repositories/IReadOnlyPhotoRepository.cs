using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IReadOnlyPhotoRepository : IRepositoryBase<Photo>
    {
        IPhotoFilterBuilder ReadBuilder { get; }
    }
}
