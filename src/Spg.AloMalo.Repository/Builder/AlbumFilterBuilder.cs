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
        private readonly List<IFilterOperation<Album>> _filters = new();

        public AlbumFilterBuilder(IQueryable<Album> albums)
        {
            EntityList = albums;
        }

        public IQueryable<Album> EntityList { get; set; }

        public IAlbumFilterBuilder ApplyIdFilter(AlbumId id)
        {
            _filters.Add(new EqualsFilter<Album>("Id", id.ToString()));
            return this;
        }

        public IAlbumFilterBuilder ApplyNameContainsFilter(string name)
        {
            _filters.Add(new ContainsFilter<Album>("Name", name));
            return this;
        }

        public IQueryable<Album> Build()
        {
            return _filters.Aggregate(EntityList, (current, filter) => filter.Apply(current));
        }
    }
}
