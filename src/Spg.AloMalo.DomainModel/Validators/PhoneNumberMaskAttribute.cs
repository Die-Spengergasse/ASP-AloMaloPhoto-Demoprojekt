using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Spg.AloMalo.DomainModel.Validators
{
    [AttributeUsage(
        AttributeTargets.Property | 
        AttributeTargets.Field | 
        AttributeTargets.Parameter, 
        AllowMultiple = false)]
    public class PhoneNumberMaskAttribute : ValidationAttribute
    {
        /// <summary>
        /// Regeln:
        /// * Zeichen:
        ///   * + optional
        ///   * / immer
        /// * Nur Zahlen
        /// * Wenn + dann 5 stellige Vorwahl
        /// * Wenn nicht + dann:
        ///   * 4 stellige Vorwahl
        ///   * muss mit 0 beginnen
        /// </summary>
        public string Mask { get; set; }

        public PhoneNumberMaskAttribute(string mask)
        {
            Mask = mask;
        }

        public override bool IsValid(object? value)
        {
            var phoneNumber = value?.ToString() ?? string.Empty;

            if (!phoneNumber.Contains('/'))
            {
                return false;
            }
            string[] parts = phoneNumber.Split('/');
            if (parts.Length != 2)
            {
                return false;
            }
            if (parts[0].StartsWith('+'))
            {

            }
            else
            {
                
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Mask);
        }
    }
}
