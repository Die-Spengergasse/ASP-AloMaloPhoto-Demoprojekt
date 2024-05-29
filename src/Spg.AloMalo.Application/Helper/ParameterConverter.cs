using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Helper
{
    public abstract class ExpressionConverter<TEntity>
        where TEntity : class
    {
        private readonly string _operator;

        public ExpressionConverter(string @operator)
        {
            _operator = @operator;
        }

        public ExpressionConverter<TEntity> ForProperty<TProperty>(
            string? queryParameter,
            Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (string.IsNullOrEmpty(queryParameter))
            {
                throw new ArgumentNullException(nameof(queryParameter));
            }
            MemberExpression? member = propertyExpression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"{propertyExpression} is a Method");
            }
            PropertyInfo? propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"{propertyExpression} is a field");
            }
            string[] parts = queryParameter.Split(' ');
            if (parts.Length != 3) 
            {
                throw new ArgumentException("Expression is incorrect (.... ct ....)");
            }
            if (parts[1].Trim().ToLower() == _operator.Trim().ToLower()) 
            { 
                return new ExpressionConverter<TEntity>(propInfo, parts[0], parts[2]);
            }
            return new ExpressionConverter<TEntity>(propInfo);
        }
    }
}