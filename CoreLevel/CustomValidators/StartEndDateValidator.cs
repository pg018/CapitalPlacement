
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CapitalPlacement.CoreLevel.CustomValidators
{
    // this validator is used to validate whether start date is before the end date
    // CAUTION => This must be placed on top of the start date property only...
    public class StartEndDateValidator: ValidationAttribute
    {
        // these are for showing the user in case of error
        private readonly string _startDisplayPropertyName;
        private readonly string _endDisplayPropertyName;
        // this property for internal json structure
        private readonly string _endPropertyName;

        public StartEndDateValidator(
            string startDisplayPropertyName,
            string endDisplayPropertyName,
            string endPropertyName)
        {
            _startDisplayPropertyName = startDisplayPropertyName;
            _endDisplayPropertyName = endDisplayPropertyName;
            _endPropertyName = endPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{_startDisplayPropertyName} is Required");
            }
            DateTime startDate = Convert.ToDateTime(value);
            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(_endPropertyName);
            if (otherProperty == null)
            {
                return new ValidationResult($"{_endDisplayPropertyName} is Required");
            }
            DateTime endDate = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
            if (startDate > endDate)
            {
                return new ValidationResult($"{_startDisplayPropertyName} cannot be greater than {_endDisplayPropertyName}");
            }
            return ValidationResult.Success;
        }
    }

}
