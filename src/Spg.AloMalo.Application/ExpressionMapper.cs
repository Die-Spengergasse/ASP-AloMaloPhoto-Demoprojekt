using System;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System.Reflection;

namespace Spg.AloMalo.Application
{
    public class ExpressionMapper<TEntity> where TEntity : class
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly string? _propertyName;
        private readonly string? _value;

        public ExpressionMapper(PropertyInfo propertyInfo, string? propertyName, string? value)
        {
            _value = value;
            _propertyInfo = propertyInfo;
            _propertyName = propertyName;
        }
        public void Use<T>(Func<T, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value)) { return; }
            if (action is null) { return; }
            T target = TConverter.ChangeType<T>(_value);
            if ((_propertyInfo?.Name?.Trim()?.ToLower() ?? string.Empty) == (_propertyName?.Trim()?.ToLower() ?? string.Empty))
            {
                action(target);
            }
        }
    }
}

