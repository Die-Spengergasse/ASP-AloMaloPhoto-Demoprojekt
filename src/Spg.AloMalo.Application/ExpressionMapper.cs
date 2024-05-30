using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application
{
    public class ExpressionMapper<TEntity>
        where TEntity : class
    {
        private readonly string? _value1;
        private readonly string? _value2;
        private readonly PropertyInfo _propertyInfo;
        private readonly string? _propertyName;

        public ExpressionMapper(PropertyInfo propertyInfo, string? propertyName = null, string? value1 = null, string? value2 = null)
        {
            _propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            _propertyName = propertyName;
            _value1 = value1;
            _value2 = value2;
        }

        public void Use<T>(Func<T, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value1) || action == null) return;

            var target1 = CustomTypeConverter.ChangeType<T>(_value1);

            if (string.Equals(_propertyInfo.Name, _propertyName, StringComparison.OrdinalIgnoreCase))
            {
                action(target1);
            }
        }

        internal void Use<T1, T2>(Func<T1, T2, IEntityFilterBuilder<TEntity>>? action)
        {
            if (string.IsNullOrEmpty(_value1) || string.IsNullOrEmpty(_value2) || action == null) return;

            var target1 = CustomTypeConverter.ChangeType<T1>(_value1);
            var target2 = CustomTypeConverter.ChangeType<T2>(_value2);

            if (string.Equals(_propertyInfo.Name, _propertyName, StringComparison.OrdinalIgnoreCase))
            {
                action(target1, target2);
            }
        }

        
    }


    public static class CustomTypeConverter
    {
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }

        public static object ChangeType(Type targetType, object value)
        {
            var converter = TypeDescriptor.GetConverter(targetType);
            return converter.ConvertFrom(value) ?? throw new InvalidOperationException($"Cannot convert {value} to {targetType}");
        }

        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
