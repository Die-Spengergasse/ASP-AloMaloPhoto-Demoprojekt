using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Helper
{
    public class ExpressionMapper<TEntity>
        where TEntity : class
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly string? _propertyName;
        private readonly string? _value;

        public ExpressionMapper(PropertyInfo propertyInfo, string? propertyName = null, string? value = null)
        {
            _propertyInfo = propertyInfo;
            _propertyName = propertyName;
            _value = value;
        }


        public void Use<T>(Func<T, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value))
            {
                return;
            }
            if (action == null)
            {
                return;
            }

            T target = TConverter.ChangeType<T>(_value);
            if ((_propertyInfo?.Name?.Trim()?.ToLower() ?? string.Empty) == (_propertyName?.Trim()?.ToLower() ?? string.Empty)) 
            { 
                action(target);
            }
        }
    }
}
