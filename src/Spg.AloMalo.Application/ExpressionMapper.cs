using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Spg.AloMalo.Application
{
    public class ExpressionMapper<TEntity>
        where TEntity : class
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly string? _propertyName;
        private readonly string? _value1;
        private readonly string? _value2;

        public ExpressionMapper(PropertyInfo propertyInfo, string? propertyName = null
            , string? value1 = null, string? value2 = null)
        {
            _propertyInfo = propertyInfo;
            _propertyName = propertyName;
            _value1 = value1;
            _value2 = value2;
        }

        public void Use<T>(Func<Expression<Func<TEntity, T>>, T, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value1))
            {
                return;
            }
            if (action is null)
            {
                return;
            }

            T target1 = TConverter.ChangeType<T>(_value1);

            if ((_propertyInfo?.Name?.Trim()?.ToLower() ?? string.Empty)
                == (_propertyName?.Trim()?.ToLower() ?? string.Empty))
            {
                action(entity => (T)_propertyInfo.GetValue(entity), target1);
            }
        }

        public void Use<T1, T2>(Func<Expression<Func<TEntity, T1>>, T1, T2, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value1) || string.IsNullOrEmpty(_value2))
            {
                return;
            }
            if (action is null)
            {
                return;
            }

            T1 target1 = TConverter.ChangeType<T1>(_value1);
            T2 target2 = TConverter.ChangeType<T2>(_value2);

            if ((_propertyInfo?.Name?.Trim()?.ToLower() ?? string.Empty)
                == (_propertyName?.Trim()?.ToLower() ?? string.Empty))
            {
                action(entity => (T1)_propertyInfo.GetValue(entity), target1, target2);
            }
        }
    }
}
