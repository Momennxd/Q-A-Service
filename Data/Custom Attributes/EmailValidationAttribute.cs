using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Custom_Attributes
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailValidationAttribute : ValidationAttribute
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public EmailValidationAttribute()
        {
            ErrorMessage = "Wrong Email Structure";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Allow null value
            if (value == null) return ValidationResult.Success;

            if (value is string strValue)
            {
                // Validate the email format using regex
                if (!Regex.IsMatch(strValue, EmailPattern))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                return new ValidationResult("Email must be a string.");
            }

            return ValidationResult.Success;
        }
    }

}
