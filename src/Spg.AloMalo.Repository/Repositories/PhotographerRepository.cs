using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Repositories
{
    public class PhotographerRepository : RepositoryBase<Photographer>, IPhotographerRepository
    {
        public PhotographerRepository(PhotoContext photoContext)
            :base(photoContext)
        { }
    }
}
