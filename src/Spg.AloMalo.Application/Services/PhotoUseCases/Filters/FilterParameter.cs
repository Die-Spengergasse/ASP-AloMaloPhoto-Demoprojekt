using System;
using System.Linq.Expressions;
using System.Reflection;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using IQueryParameter = Spg.AloMalo.Application.Services.PhotoUseCases.Query.IQueryParameter;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filters;

public class FilterParameter : IQueryParameter
{
    private readonly IPhotoFilterBuilder _photoFilterBuilder;

    public FilterParameter(IPhotoFilterBuilder photoFilterBuilder)
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

            var propertyInfo = typeof(Photo).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                switch (operation)
                {
                    case "eq":
                        if (TryParseValue(value, propertyType, out var equalValue))
                        {
                            return _photoFilterBuilder.ApplyFilter(CreateEqualsFilter(propertyInfo, equalValue));
                        }
                        break;
                    case "ne":
                        if (TryParseValue(value, propertyType, out var notEqualValue))
                        {
                            return _photoFilterBuilder.ApplyFilter(CreateNotEqualsFilter(propertyInfo, notEqualValue));
                        }
                        break;
                    case "ct":
                        return _photoFilterBuilder.ApplyFilter(CreateContainsFilter(propertyInfo, value));
                    case "sw":
                        return _photoFilterBuilder.ApplyFilter(CreateStartsWithFilter(propertyInfo, value));
                    case "ew":
                        return _photoFilterBuilder.ApplyFilter(CreateEndsWithFilter(propertyInfo, value));
                    case "regex":
                        return _photoFilterBuilder.ApplyFilter(CreateRegexFilter(propertyInfo, value));
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

    private IFilters<Photo> CreateNotEqualsFilter(PropertyInfo propertyInfo, object value)
    {
        var parameter = Expression.Parameter(typeof(Photo), "p");
        var property = Expression.Property(parameter, propertyInfo);
        var constantValue = Expression.Constant(value);
        var notEqualsExpression = Expression.NotEqual(property, constantValue);
        var lambda = Expression.Lambda<Func<Photo, bool>>(notEqualsExpression, parameter);

        return new NotEqualsFilter<Photo>(lambda, value);
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

private IFilters<Photo> CreateStartsWithFilter(PropertyInfo propertyInfo, string value)
{
    var parameter = Expression.Parameter(typeof(Photo), "p");
    var property = Expression.Property(parameter, propertyInfo);
    var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
    var constantValue = Expression.Constant(value);
    var startsWithExpression = Expression.Call(property, method, constantValue);
    var lambda = Expression.Lambda<Func<Photo, bool>>(startsWithExpression, parameter);

    return new StartsWithFilter<Photo>(lambda, value);
}

private IFilters<Photo> CreateEndsWithFilter(PropertyInfo propertyInfo, string value)
{
    var parameter = Expression.Parameter(typeof(Photo), "p");
    var property = Expression.Property(parameter, propertyInfo);
    var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
    var constantValue = Expression.Constant(value);
    var endsWithExpression = Expression.Call(property, method, constantValue);
    var lambda = Expression.Lambda<Func<Photo, bool>>(endsWithExpression, parameter);

    return new EndsWithFilter<Photo>(lambda, value);
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