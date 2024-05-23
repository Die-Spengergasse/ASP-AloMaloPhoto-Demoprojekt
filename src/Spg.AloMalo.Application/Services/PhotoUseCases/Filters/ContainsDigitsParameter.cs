using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class ContainsDigitsParameter : IQueryParameter
{
    private readonly IPhotoFilterBuilder _photoFilterBuilder;

    public ContainsDigitsParameter(IPhotoFilterBuilder photoFilterBuilder)
    {
        _photoFilterBuilder = photoFilterBuilder;
    }

    public IPhotoFilterBuilder Compile(string queryParameter)
    {
        string[] parts = queryParameter.Split(' ');
        if (parts.Length == 2)
        {
            string propertyName = parts[0]?.Trim().ToLower();
            string operation = parts[1]?.Trim().ToLower();

            if (operation == "containsdigits")
            {
                var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                {
                    return _photoFilterBuilder.ApplyFilter(CreateContainsDigitsFilter(propertyInfo));
                }
            }
        }
        return _photoFilterBuilder;
    }

    private IFilters<Photo> CreateContainsDigitsFilter(PropertyInfo propertyInfo)
    {
        var parameter = Expression.Parameter(typeof(Photo), "p");
        var property = Expression.Property(parameter, propertyInfo);
        var regexType = typeof(System.Text.RegularExpressions.Regex);
        var isMatchMethod = regexType.GetMethod("IsMatch", new[] { typeof(string), typeof(string) });
        var patternConstant = Expression.Constant(@"\d");
        var regexExpression = Expression.Call(isMatchMethod, property, patternConstant);
        var lambda = Expression.Lambda<Func<Photo, bool>>(regexExpression, parameter);

        return new ContainsDigitsFilter<Photo>(lambda);
    }
}