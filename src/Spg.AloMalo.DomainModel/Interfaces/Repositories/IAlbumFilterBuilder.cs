using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Interfaces.Repositories
{
    public interface IAlbumFilterBuilder : IEntityReaderBuilder<Album>
    {
        new IQueryable<Album> EntityList { get; set; }

        IAlbumFilterBuilder ApplyIdFilter(AlbumId id);
        IAlbumFilterBuilder ApplyNameContainsFilter(string name);

        new IQueryable<Album> Build();
    }
}
