using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.CustomValidators
{
    // this validator is used to validate if the location is entered, then it is not remote and vice versa
    // CAUTION => This must be put above location property only
    public class LocationOrRemoteValidator: ValidationAttribute
    {
        private readonly string _remotePropertyName;
        private readonly string _remoteDisplayPropertyName;

        public LocationOrRemoteValidator(string remotePropertyName, string remoteDisplayPropertyName)
        {
            _remotePropertyName = remotePropertyName;
            _remoteDisplayPropertyName= remoteDisplayPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? locationValue = (string?)value;
            var remoteProperty = validationContext.ObjectType.GetProperty(_remotePropertyName);
            if (remoteProperty == null || remoteProperty.PropertyType != typeof(bool)) 
            {
                return new ValidationResult($"Invalid Property! {_remoteDisplayPropertyName}");
            }
            bool fullyRemote = (bool)remoteProperty.GetValue(validationContext.ObjectInstance, null)!;
            if (string.IsNullOrWhiteSpace(locationValue) && fullyRemote)
            {
                // if location is empty and fully remote is selected
                return ValidationResult.Success;
            }
            if (!string.IsNullOrWhiteSpace(locationValue) && !fullyRemote)
            {
                // if location is filled and fully remote is not selected
                return ValidationResult.Success;
            }
            if (!string.IsNullOrWhiteSpace(locationValue) && fullyRemote)
            {
                // location is filled and fully remote is also selected
                return new ValidationResult($"Location Cannot be specified if program is {_remoteDisplayPropertyName}");
            }
            // string is null and remote is false;
            return new ValidationResult($"Location Must Be Entered");
        }
    }
}
