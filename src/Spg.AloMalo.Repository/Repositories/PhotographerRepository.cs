using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.Repository.Repositories
{
    public class PhotographerRepository : RepositoryBase<Photographer>, IPhotographerRepository
    {
        public PhotographerRepository(PhotoContext photoContext)
            :base(photoContext)
        { }
    }
}
