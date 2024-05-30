using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Filter;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filter
{
    public class PropertyFilterParameter<T> : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        private readonly Expression<Func<T, object>> _propertyExpression;

        public PropertyFilterParameter(IPhotoFilterBuilder photoFilterBuilder, Expression<Func<T, object>> propertyExpression)
        {
            _photoFilterBuilder = photoFilterBuilder ?? throw new ArgumentNullException(nameof(photoFilterBuilder));
            _propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            if (string.IsNullOrWhiteSpace(queryParameter))
                throw new ArgumentException("Query parameter cannot be null or whitespace.", nameof(queryParameter));

            var parts = queryParameter.Split(' ');
            if (parts.Length != 3)
                throw new ArgumentException("Wrong query parameter format. Should be: 'property operation value'");

            var property = parts[0];
            var operation = parts[1].ToLower();
            var value = parts[2];

            if (property != ((MemberExpression)_propertyExpression.Body).Member.Name.ToLower())
                throw new ArgumentException($"Invalid property: {property}");

            return operation switch
            {
                "equals" => ApplyEqualsFilter(value),
                "contains" => ApplyContainsFilter(value),
                "startswith" => ApplyStartsWithFilter(value),
                "endswith" => ApplyEndsWithFilter(value),
                _ => throw new InvalidOperationException("Unknown operation")
            };
        }

        private IPhotoFilterBuilder ApplyStartsWithFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("StartsWith operation can only be applied to string properties.");

            var filter = new StartsWithFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            return _photoFilterBuilder.ApplyFilter(filter);
        }

        private IPhotoFilterBuilder ApplyEndsWithFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("EndsWith operation can only be applied to string properties.");

            var filter = new EndsWithFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            return _photoFilterBuilder.ApplyFilter(filter);
        }

        private IPhotoFilterBuilder ApplyEqualsFilter(string value)
        {
            var propertyType = _propertyExpression.Body.Type;
            var convertedValue = Convert.ChangeType(value, propertyType);
            var filterType = typeof(EqualsFilter<,>).MakeGenericType(typeof(Photo), propertyType);
            var filter = (IFilter<Photo>)Activator.CreateInstance(filterType, _propertyExpression, convertedValue);
            return _photoFilterBuilder.ApplyFilter(filter);
        }

        private IPhotoFilterBuilder ApplyContainsFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("Contains operation can only be applied to string properties.");

            var filter = new ContainsFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            return _photoFilterBuilder.ApplyFilter(filter);
        }
    }
}
