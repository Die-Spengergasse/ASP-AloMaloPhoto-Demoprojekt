using System.Linq.Expressions;
using System.Reflection;

namespace Spg.AloMalo.Application
{
    public abstract class InterpretParameterBase<TEntity>
        where TEntity : class
    {
        private readonly string _operator;

        public InterpretParameterBase(string @operator)
        {
            _operator = @operator;
        }

        public ExpressionMapper<TEntity> ForProperty<TProperty>(
            string? queryParameter,
            Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            MemberExpression? member = propertyExpression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"{propertyExpression} is a Method!");
            }
            PropertyInfo? propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"{propertyExpression} is a Field!");
            }

            if (string.IsNullOrEmpty(queryParameter))
            {
                return new ExpressionMapper<TEntity>(propInfo);
            }
            string[] parts = queryParameter.Split(' ');
            if (parts.Length < 3 || parts.Length > 4)
            {
                throw new ArgumentException($"Incorrect Expression ('xxxx ct xxxx' or 'xxxx bw xxxx xxxx')");
            }
            if (parts[1].Trim().ToLower() == _operator.Trim().ToLower())
            {
                if (parts.Length == 3)
                {
                    return new ExpressionMapper<TEntity>(propInfo, parts[0], parts[2]);
                }
                if (parts.Length == 4)
                {
                    return new ExpressionMapper<TEntity>(propInfo, parts[0], parts[2], parts[3]);
                }
            }
            return new ExpressionMapper<TEntity>(propInfo);
        }
    }
}
