
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.CustomValidators
{
    // this validator is used to validate if string value is entered correctly and used to check for null val
    public class DefaultStringValidator: ValidationAttribute
    {
        private readonly string _property;

        public DefaultStringValidator(string property)
        {
            _property = property;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{_property} is Missing");
            }
            string stringVal = value.ToString();
            if (string.IsNullOrEmpty(stringVal?.Trim()))
            {
                return new ValidationResult($"{_property} is Required");
            }
            return ValidationResult.Success;
        }
    }
}
