using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Filters;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System.Linq.Expressions;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class PropertyFilterParameter<T> : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        private readonly Expression<Func<T, object>> _propertyExpression;
        private readonly string _operation;
        private readonly string _value;

        public PropertyFilterParameter(IPhotoFilterBuilder photoFilterBuilder, Expression<Func<T, object>> propertyExpression, string operation, string value)
        {
            _photoFilterBuilder = photoFilterBuilder;
            _propertyExpression = propertyExpression;
            _operation = operation;
            _value = value;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            var parts = queryParameter.Split(' ');
            if (parts.Length != 3)
                throw new ArgumentException("Invalid query parameter format. Expected format: 'property operation value'");

            var property = parts[0];
            var operation = parts[1];
            var value = parts[2];

            switch (operation.ToLower())
            {
                case "equals":
                    ApplyEqualsFilter(value);
                    break;
                case "contains":
                    ApplyContainsFilter(value);
                    break;
                case "startswith":
                    ApplyStartsWithFilter(value);
                    break;
                case "endswith":
                    ApplyEndsWithFilter(value);
                    break;
                case "regex":
                    ApplyRegexFilter(value);
                    break;
                default:
                    throw new InvalidOperationException("Unknown operation");
            }
            return _photoFilterBuilder;
        }

        private void ApplyEqualsFilter(string value)
        {
            var propertyType = _propertyExpression.Body.Type;
            var convertedValue = Convert.ChangeType(value, propertyType);
            var filterType = typeof(EqualsFilter<,>).MakeGenericType(typeof(Photo), propertyType);
            var filter = (EqualsFilter<Photo, object>)Activator.CreateInstance(filterType, _propertyExpression, convertedValue);
            _photoFilterBuilder.ApplyFilter(filter);
        }

        private void ApplyContainsFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("Contains operation is only supported for string properties");

            var filter = new ContainsFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            _photoFilterBuilder.ApplyFilter(filter);
        }

        private void ApplyStartsWithFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("StartsWith operation is only supported for string properties");

            var filter = new StartsWithFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            _photoFilterBuilder.ApplyFilter(filter);
        }

        private void ApplyEndsWithFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("EndsWith operation is only supported for string properties");

            var filter = new EndsWithFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            _photoFilterBuilder.ApplyFilter(filter);
        }

        private void ApplyRegexFilter(string value)
        {
            if (_propertyExpression.Body.Type != typeof(string))
                throw new InvalidOperationException("Regex operation is only supported for string properties");

            var filter = new RegexFilter<Photo>(_propertyExpression as Expression<Func<Photo, string>>, value);
            _photoFilterBuilder.ApplyFilter(filter);
        }

    }
}
