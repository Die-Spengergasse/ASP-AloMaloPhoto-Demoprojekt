using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Spg.AloMalo.Application
{
	public abstract class InterpretParameterBase<TEntity> where TEntity : class
    {
        private readonly string _operation;
        public InterpretParameterBase(string operation)
        {
            _operation = operation;
        }
        public ExpressionMapper<TEntity> ForProperty<TProperty>(string queryParam, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            MemberExpression? member = propertyExpression.Body as MemberExpression;
            PropertyInfo? info = member?.Member as PropertyInfo;
            string[] parts = queryParam.Split(' ');

            if (parts.Length != 3 || info is null)
            {
                throw new ArgumentException("Error");
            }
            if (parts[1].Trim().ToLower() == _operation.Trim().ToLower())
            {
                return new ExpressionMapper<TEntity>(info, parts[0], parts[2]);
            }
            return new ExpressionMapper<TEntity>(info, null, null);
        }
    }
}

