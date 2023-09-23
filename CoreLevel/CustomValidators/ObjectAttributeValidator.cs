
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.CustomValidators
{
    public class ObjectAttributeValidator: ValidationAttribute
    {
        // this validation attribute is used to validate all the properties within the object
        private readonly bool _nullValueAllowed;
        private readonly string PropertyName;

        public ObjectAttributeValidator(string propertyName, bool nullValueAllowed=false)
        {
            _nullValueAllowed = nullValueAllowed;
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (_nullValueAllowed)
                {
                    // if the object is optional, but if entered then have to validate the entire object
                    return ValidationResult.Success;
                }
                return new ValidationResult($"{PropertyName} is Missing");
            }
            var nestedObjValidator = new ValidationContext(value);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(value, nestedObjValidator, results, true);
            if (results.Count > 0)
            {
                // returning all error messages together
                var errorMessage = string.Join(Environment.NewLine, results.Select(e => e.ErrorMessage));
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
