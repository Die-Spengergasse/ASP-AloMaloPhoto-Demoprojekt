using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _operation = operation;
            _value = value;
        }

        public IPhotoFilterBuilder Apply()
        {
            switch (_operation.ToLower())
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
                    return _photoFilterBuilder.ApplyGreaterThanFilter(_propertyName, _value);
                case "greaterthanequal":
                    return _photoFilterBuilder.ApplyGreaterThanOrEqualFilter(_propertyName, _value);
                case "lt":
                case "lessthan":
                    return _photoFilterBuilder.ApplyLessThanFilter(_propertyName, _value);
                case "lte":
                case "lessthanequal":
                    return _photoFilterBuilder.ApplyLessThanOrEqualFilter(_propertyName, _value);
                case "containsspecialchars":
                    return _photoFilterBuilder.ApplyContainsSpecialCharsFilter(_propertyName);
                default:
                    throw new NotSupportedException($"Operation '{_operation}' is not supported.");
            }
        }
    }

}
