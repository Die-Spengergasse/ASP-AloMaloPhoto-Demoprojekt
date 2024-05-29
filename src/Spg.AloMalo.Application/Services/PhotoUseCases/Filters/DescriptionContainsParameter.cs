using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class DescriptionContainsParameter : IQueryParameter
{
    private readonly IPhotoFilterBuilder _photoFilterBuilder;

    public DescriptionContainsParameter(IPhotoFilterBuilder photoFilterBuilder)
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

            if (propertyName == "description" && operation == "ct")
            {
                var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    return _photoFilterBuilder.ApplyFilter(CreateContainsFilter(propertyInfo, value));
                }
            }
        }
        return _photoFilterBuilder;
    }

    private IFilters<Photo> CreateContainsFilter(PropertyInfo propertyInfo, string value)
    {
        var parameter = Expression.Parameter(typeof(Photo), "p");
        var property = Expression.Property(parameter, propertyInfo);
        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var constantValue = Expression.Constant(value);
        var containsExpression = Expression.Call(property, method, constantValue);
        var lambda = Expression.Lambda<Func<Photo, bool>>(containsExpression, parameter);

        return new ContainsFilter<Photo>(lambda, value);
    }
}