using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application
{
    public abstract class ParameterBase<TEntity>
        where TEntity : class
    {
        private readonly string _operator;

        protected ParameterBase(string @operator)
        {
            _operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        }

        public ExpressionMapper<TEntity> ForProperty<TProperty>(string? queryParameter, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (propertyExpression.Body is not MemberExpression member)
                throw new ArgumentException($"{propertyExpression} is not a member expression!");

            if (member.Member is not PropertyInfo propInfo)
                throw new ArgumentException($"{propertyExpression} does not refer to a property!");

            if (string.IsNullOrEmpty(queryParameter))
                return new ExpressionMapper<TEntity>(propInfo);

            var parts = queryParameter.Split(' ');
            if (parts.Length < 3 || parts.Length > 4)
                throw new ArgumentException("Incorrect expression format ('property operator value' or 'property between value1 value2')");

            if (string.Equals(parts[1], _operator, StringComparison.OrdinalIgnoreCase))
            {
                return parts.Length switch
                {
                    3 => new ExpressionMapper<TEntity>(propInfo, parts[0], parts[2]),
                    4 => new ExpressionMapper<TEntity>(propInfo, parts[0], parts[2], parts[3]),
                    _ => new ExpressionMapper<TEntity>(propInfo)
                };
            }

            return new ExpressionMapper<TEntity>(propInfo);
        }
    }
}
