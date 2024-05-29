using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters
{
    public class PropertyFilterParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;
        private readonly FilterParameter _filterParameter;

        public PropertyFilterParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
            _filterParameter = new FilterParameter(photoFilterBuilder);
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            if (parts.Length == 3)
            {
                string propertyName = parts[0]?.Trim().ToLower();
                string operation = parts[1]?.Trim().ToLower();
                string value = parts[2];
                var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    // Use propertyInfo in the CreateEqualsFilter method
                    return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, value));
                }
                var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                if (propertyType == typeof(string))
                {
                    return _filterParameter.Compile(queryParameter);
                }
                else if (propertyType == typeof(int) || propertyType == typeof(long) ||
                         propertyType == typeof(decimal) || propertyType == typeof(double) ||
                         propertyType == typeof(float))
                {
                    if (operation == "eq" && TryParseValue(value, propertyType, out var numericValue))
                    {
                        return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, numericValue));
                    }
                }
                else if (propertyType == typeof(bool))
                {
                    if (operation == "eq" && bool.TryParse(value, out var boolValue))
                    {
                        return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, boolValue));
                    }
                }
                else if (propertyType == typeof(DateTime))
                {
                    if (operation == "eq" && DateTime.TryParse(value, out var dateTimeValue))
                    {
                        return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, dateTimeValue));
                    }
                }
                else if (propertyType.IsEnum)
                {
                    if (operation == "eq" && Enum.TryParse(propertyType, value, true, out var enumValue))
                    {
                        return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, enumValue));
                    }
                }
            }

            return _photoFilterBuilder;
        }

        private IFilters<Photo> CreateEqualsFilter(PropertyInfo propertyInfo, object value)
        {
            var parameter = Expression.Parameter(typeof(Photo), "p");
            var property = Expression.Property(parameter, propertyInfo);
            var constantValue = Expression.Constant(value);
            var equalsExpression = Expression.Equal(property, constantValue);
            var lambda = Expression.Lambda<Func<Photo, bool>>(equalsExpression, parameter);

            return new EqualsFilter<Photo>(lambda, value);
        }

        private bool TryParseValue(string value, Type propertyType, out object parsedValue)
        {
            parsedValue = null;

            try
            {
                parsedValue = Convert.ChangeType(value, propertyType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}