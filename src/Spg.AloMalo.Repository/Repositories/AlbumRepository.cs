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
    public class AlbumRepository : ReadOnlyRepository<Album, IAlbumFilterBuilder>, 
        IReadOnlyAlbumRepository
    {
        public AlbumRepository(PhotoContext photoContext,
            IAlbumFilterBuilder readBuilder)
                : base(photoContext, readBuilder)
        { }

        public IQueryable<Album> GetAll()
        {
            IQueryable<Album> data = FilterBuilder.Build();
            return data;
        }
    }
}
