using Spg.AloMalo.DomainModel.Entities;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Repository.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        private List<Func<Photo, bool>> _filters = new List<Func<Photo, bool>>();
        private GenericFilter<Photo> _genericFilter = new GenericFilter<Photo>();

        public IPhotoFilterBuilder WithLastName(string lastName)
        {
            _filters.Add(_genericFilter.EqualsFilter(nameof(Photo.LastName), lastName));
            return this;
        }

        public IPhotoFilterBuilder WithFirstName(string firstName)
        {
            _filters.Add(_genericFilter.EqualsFilter(nameof(Photo.FirstName), firstName));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyEquals(string propertyName, object value)
        {
            _filters.Add(_genericFilter.EqualsFilter(propertyName, value));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyContains(string propertyName, string value)
        {
            _filters.Add(_genericFilter.ContainsFilter(propertyName, value));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyStartsWith(string propertyName, string value)
        {
            _filters.Add(_genericFilter.StartsWithFilter(propertyName, value));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyEndsWith(string propertyName, string value)
        {
            _filters.Add(_genericFilter.EndsWithFilter(propertyName, value));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyRegex(string propertyName, string pattern)
        {
            _filters.Add(_genericFilter.RegexFilter(propertyName, pattern));
            return this;
        }

        public IPhotoFilterBuilder WithPropertyContainsDigits(string propertyName)
        {
            _filters.Add(_genericFilter.ContainsDigitsFilter(propertyName));
            return this;
        }

        public IPhotoFilterBuilder Not(Func<Photo, bool> filter)
        {
            _filters.Add(_genericFilter.NotFilter(filter));
            return this;
        }

        public IEnumerable<Photo> Apply(IEnumerable<Photo> query)
        {
            foreach (var filter in _filters)
            {
                query = query.Where(filter);
            }
            return query;
        }
    }
}
