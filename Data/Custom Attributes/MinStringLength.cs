using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Custom_Attributes
{

    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinStringLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinStringLengthAttribute(int minLength)
        {
            _minLength = minLength;
            ErrorMessage = $"The field must be at least {_minLength} characters long.";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false; 
            if (value is string strValue) return strValue.Length >= _minLength;

            return false;
        }
    }




}
