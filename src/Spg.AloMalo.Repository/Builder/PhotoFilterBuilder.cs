using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Repository.Repositories;
using System.Text.RegularExpressions;

namespace Spg.AloMalo.Repository.Builder
{
    public class PhotoFilterBuilder : IPhotoFilterBuilder
    {
        public IQueryable<Photo> EntityList { get; set; }

        private readonly List<Func<Photo, bool>> _conditions = new List<Func<Photo, bool>>();

        public PhotoFilterBuilder(IQueryable<Photo> photos)
        {
            EntityList = photos;
        }

        public IQueryable<Photo> Build()
        {
            return EntityList.Where(photo => _conditions.All(condition => condition(photo)));
        }

        public IPhotoFilterBuilder ApplyEqualsFilter(string propertyName, string value)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).Equals(value, StringComparison.OrdinalIgnoreCase));
            return this;
        }

        public IPhotoFilterBuilder ApplyContainsFilter(string propertyName, string value)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).Contains(value, StringComparison.OrdinalIgnoreCase));
            return this;
        }

        public IPhotoFilterBuilder ApplyStartsWithFilter(string propertyName, string value)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).StartsWith(value, StringComparison.OrdinalIgnoreCase));
            return this;
        }

        public IPhotoFilterBuilder ApplyEndsWithFilter(string propertyName, string value)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).EndsWith(value, StringComparison.OrdinalIgnoreCase));
            return this;
        }

        public IPhotoFilterBuilder ApplyContainsDigitsFilter(string propertyName)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).Any(char.IsDigit));
            return this;
        }

        public IPhotoFilterBuilder ApplyGreaterThanFilter(string propertyName, string value)
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                _conditions.Add(photo => decimal.TryParse(GetPropertyValue(photo, propertyName), out var propValue) && propValue > decimalValue);
            }
            return this;
        }

        public IPhotoFilterBuilder ApplyGreaterThanOrEqualFilter(string propertyName, string value)
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                _conditions.Add(photo => decimal.TryParse(GetPropertyValue(photo, propertyName), out var propValue) && propValue >= decimalValue);
            }
            return this;
        }

        public IPhotoFilterBuilder ApplyLessThanFilter(string propertyName, string value)
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                _conditions.Add(photo => decimal.TryParse(GetPropertyValue(photo, propertyName), out var propValue) && propValue < decimalValue);
            }
            return this;
        }

        public IPhotoFilterBuilder ApplyLessThanOrEqualFilter(string propertyName, string value)
        {
            if (decimal.TryParse(value, out var decimalValue))
            {
                _conditions.Add(photo => decimal.TryParse(GetPropertyValue(photo, propertyName), out var propValue) && propValue <= decimalValue);
            }
            return this;
        }

        public IPhotoFilterBuilder ApplyContainsSpecialCharsFilter(string propertyName)
        {
            _conditions.Add(photo => GetPropertyValue(photo, propertyName).Any(ch => !char.IsLetterOrDigit(ch)));
            return this;
        }

        private string GetPropertyValue(Photo photo, string propertyName)
        {
            var property = typeof(Photo).GetProperty(propertyName);
            return property?.GetValue(photo)?.ToString() ?? string.Empty;
        }
    }
}
