using Bogus.DataSets;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Repository.Builder
{
    public class AlbumFilterBuilder : IEntityFilterBuilder<Album>, IAlbumFilterBuilder
    {
        public IQueryable<Album> EntityList { get; set; }

        public AlbumFilterBuilder(IQueryable<Album> albums)
        {
            EntityList = albums;
        }

        public IAlbumFilterBuilder ApplyIdFilter(AlbumId id)
        {
            EntityList = EntityList.Where(x => x.Id == id);
            return this;
        }

        public IAlbumFilterBuilder ApplyNameContainsFilter(string name)
        {
            EntityList = EntityList.Where(x => x.Name.Contains(name));
            return this;
        }

        public IQueryable<Album> Build()
        {
            return EntityList;
        }
    }
}
