using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Linq;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoPropertyFilter : IPhotoPropertyFilter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        private readonly string _propertyName;
        private readonly string _operation;
        private readonly string _value;

        public PhotoPropertyFilter(IPhotoFilterBuilder photoFilterBuilder, string propertyName, string operation, string value)
        {
            _photoFilterBuilder = photoFilterBuilder;
            _propertyName = propertyName;
            _operation = operation.ToLower();
            _value = value;
        }

        public IPhotoFilterBuilder Apply()
        {
            switch (_operation)
            {
                case "equals":
                    return _photoFilterBuilder.ApplyEqualsFilter(_propertyName, _value);
                case "contains":
                    return _photoFilterBuilder.ApplyContainsFilter(_propertyName, _value);
                case "startswith":
                    return _photoFilterBuilder.ApplyStartsWithFilter(_propertyName, _value);
                case "endswith":
                    return _photoFilterBuilder.ApplyEndsWithFilter(_propertyName, _value);
                case "containsdigits":
                    return _photoFilterBuilder.ApplyContainsDigitsFilter(_propertyName);
                case "greaterthan":
                case "greaterthanequal":
                    return ApplyNumericComparison(_operation, _propertyName, _value);
                case "lt":
                case "lessthan":
                case "lte":
                case "lessthanequal":
                    return ApplyNumericComparison(_operation, _propertyName, _value);
                case "containsspecialchars":
                    return _photoFilterBuilder.ApplyContainsSpecialCharsFilter(_propertyName);
                default:
                    throw new NotSupportedException($"Operation '{_operation}' is not supported.");
            }
        }

        private IPhotoFilterBuilder ApplyNumericComparison(string operation, string propertyName, string value)
        {
            switch (operation)
            {
                case "greaterthan":
                    return _photoFilterBuilder.ApplyGreaterThanFilter(propertyName, value);
                case "greaterthanequal":
                    return _photoFilterBuilder.ApplyGreaterThanOrEqualFilter(propertyName, value);
                case "lt":
                case "lessthan":
                    return _photoFilterBuilder.ApplyLessThanFilter(propertyName, value);
                case "lte":
                case "lessthanequal":
                    return _photoFilterBuilder.ApplyLessThanOrEqualFilter(propertyName, value);
                default:
                    throw new InvalidOperationException($"Numeric operation '{operation}' is not properly defined.");
            }
        }
    }
}
