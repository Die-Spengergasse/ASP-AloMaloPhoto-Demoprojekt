using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }
        private readonly List<IFilters<Photo>> _filters = new();

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }
        

        public IPhotoFilterBuilder ApplyFilter(IFilters<Photo> filter)
        {
            _filters.Add(filter);
            return this;
        }
        
        public IQueryable<Photo> Build()
        {
            var query = EntityList;
            foreach (var filter in _filters)
            {
                query = filter.Apply(query);
            }
            return query;
        }
    }
}
