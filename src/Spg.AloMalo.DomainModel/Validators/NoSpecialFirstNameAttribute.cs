using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Validators
{
    public class NoSpecialFirstNameAttribute : ValidationAttribute, IModelEntityValidator
    {
        public string ForbiddenName{ get; set; }

        public NoSpecialFirstNameAttribute(string forbiddenName)
        {
            ForbiddenName = forbiddenName;
        }

        public override bool IsValid(object? value)
        {
            if (value?.ToString()?.ToLower()
                .Contains(ForbiddenName?.ToLower() ?? string.Empty) 
                ?? false)
            {
                return false;
            }
            return true;
        }
    }
}
