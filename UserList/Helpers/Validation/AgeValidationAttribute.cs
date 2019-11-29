using System;
using System.ComponentModel.DataAnnotations;

namespace UserList.Helpers.Validation
{
    public class AgeValidationAttribute : ValidationAttribute
    {
        private readonly int _limit;

        public AgeValidationAttribute(int limit)
        {
            _limit = limit;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var bd = (DateTime) value;
            var dtNow = DateTime.Now;

            return bd.AddYears(_limit) <= dtNow ? ValidationResult.Success : new ValidationResult("You must be 18 or more years old");
        }
    }
}
