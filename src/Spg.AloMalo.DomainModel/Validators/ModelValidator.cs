using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Validators
{
    public class ModelValidator<TEntity>
        where TEntity : class
    {
        public bool Validate(TEntity entity)
        {
            bool isValid = true;

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo property in properties) 
            {
                IEnumerable<Attribute> attr = property.GetCustomAttributes();
                foreach (Attribute attribute in attr) 
                {
                    if (attribute?.GetType()?.BaseType?.Name != nameof(ValidationAttribute))
                    {
                        continue;
                    }
                    ValidationAttribute modelEntityValidator = (ValidationAttribute)attribute;
                    if (modelEntityValidator is null)
                    {
                        continue;
                    }
                    object? val = property.GetValue(entity);
                    isValid = modelEntityValidator.IsValid(val);
                }
            }
            return isValid;
        }
    }
}
