using Spg.AloMalo.DomainModel.Dtos;
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
        ErrorCheck<IQueryable<AlbumDto>> GetAllOk();
        ErrorCheck<IQueryable<AlbumDto>> GetAll400();
        ErrorCheck<IQueryable<AlbumDto>> GetAll404();
    }
}
