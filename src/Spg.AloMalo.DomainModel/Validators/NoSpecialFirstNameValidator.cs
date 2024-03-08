using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.DomainModel.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoSpecialFirstNameValidator : ValidationAttribute
    {
        public string ForbiddenName { get; set; }

        public NoSpecialFirstNameValidator(string forbiddenName)
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

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            return new ValidationResult(null);
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name, ForbiddenName);
        }
    }
}
