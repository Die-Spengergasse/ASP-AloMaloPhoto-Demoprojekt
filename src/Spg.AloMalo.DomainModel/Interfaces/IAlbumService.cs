using Spg.AloMalo.DomainModel.Error;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces
{
    public interface IAlbumService
    {
        ErrorCheck<IQueryable<Album>> GetAllOk();
        ErrorCheck<IQueryable<Album>> GetAll400();
        ErrorCheck<IQueryable<Album>> GetAll404();
    }
}
