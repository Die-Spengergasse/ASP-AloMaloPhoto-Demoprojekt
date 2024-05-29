using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class ImageTypeEqualsParameter : IQueryParameter
{
    private readonly IPhotoFilterBuilder _photoFilterBuilder;

    public ImageTypeEqualsParameter(IPhotoFilterBuilder photoFilterBuilder)
    {
        _photoFilterBuilder = photoFilterBuilder;
    }

    public IPhotoFilterBuilder Compile(string queryParameter)
    {
        string[] parts = queryParameter.Split(' ');
        if (parts.Length == 3)
        {
            string propertyName = parts[0]?.Trim().ToLower();
            string operation = parts[1]?.Trim().ToLower();
            string value = parts[2];

            if (propertyName == "imagetype" && operation == "eq")
            {
                var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(ImageTypes))
                {
                    if (Enum.TryParse<ImageTypes>(value, true, out var imageType))
                    {
                        return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, imageType));
                    }
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
}