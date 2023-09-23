
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.CustomValidators
{
    // this validator is responsible for check if the value is null (exists) or not
    public class NonNullValidator: ValidationAttribute
    {
        private readonly string _propertyName;

        public NonNullValidator(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{_propertyName} Attribute is Missing");
            }
            return ValidationResult.Success;
        }
    }
}
