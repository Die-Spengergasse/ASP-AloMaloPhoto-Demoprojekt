using Spg.AloMalo.DomainModel.Entities;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.AloMalo.Repository.Builder
{
    public class AlbumFilterBuilder : IAlbumFilterBuilder
    {
        private List<Func<Album, bool>> _filters = new List<Func<Album, bool>>();
        private GenericFilter<Album> _genericFilter = new GenericFilter<Album>();

        public IAlbumFilterBuilder WithTitle(string title)
        {
            _filters.Add(_genericFilter.EqualsFilter(nameof(Album.Title), title));
            return this;
        }

        public IAlbumFilterBuilder WithArtist(string artist)
        {
            _filters.Add(_genericFilter.EqualsFilter(nameof(Album.Artist), artist));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyEquals(string propertyName, object value)
        {
            _filters.Add(_genericFilter.EqualsFilter(propertyName, value));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyContains(string propertyName, string value)
        {
            _filters.Add(_genericFilter.ContainsFilter(propertyName, value));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyStartsWith(string propertyName, string value)
        {
            _filters.Add(_genericFilter.StartsWithFilter(propertyName, value));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyEndsWith(string propertyName, string value)
        {
            _filters.Add(_genericFilter.EndsWithFilter(propertyName, value));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyRegex(string propertyName, string pattern)
        {
            _filters.Add(_genericFilter.RegexFilter(propertyName, pattern));
            return this;
        }

        public IAlbumFilterBuilder WithPropertyContainsDigits(string propertyName)
        {
            _filters.Add(_genericFilter.ContainsDigitsFilter(propertyName));
            return this;
        }

        public IAlbumFilterBuilder Not(Func<Album, bool> filter)
        {
            _filters.Add(_genericFilter.NotFilter(filter));
            return this;
        }

        public IEnumerable<Album> Apply(IEnumerable<Album> query)
        {
            foreach (var filter in _filters)
            {
                query = query.Where(filter);
            }
            return query;
        }
    }
}
