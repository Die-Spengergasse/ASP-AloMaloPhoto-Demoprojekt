using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class RegexParameter : IQueryParameter
{
    private readonly IPhotoFilterBuilder _photoFilterBuilder;

    public RegexParameter(IPhotoFilterBuilder photoFilterBuilder)
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

            if (operation == "regex")
            {
                var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                {
                    return _photoFilterBuilder.ApplyFilter(CreateRegexFilter(propertyInfo, value));
                }
            }
        }
        return _photoFilterBuilder;
    }

    private IFilters<Photo> CreateRegexFilter(PropertyInfo propertyInfo, string pattern)
    {
        var parameter = Expression.Parameter(typeof(Photo), "p");
        var property = Expression.Property(parameter, propertyInfo);
        var regexType = typeof(System.Text.RegularExpressions.Regex);
        var isMatchMethod = regexType.GetMethod("IsMatch", new[] { typeof(string), typeof(string) });
        var patternConstant = Expression.Constant(pattern);
        var regexExpression = Expression.Call(isMatchMethod, property, patternConstant);
        var lambda = Expression.Lambda<Func<Photo, bool>>(regexExpression, parameter);

        return new RegexFilter<Photo>(lambda, pattern);
    }
}