
using CapitalPlacement.CoreLevel.DTO;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.Services
{
    public class CommonService: ICommonService
    {
        private OutgoingErrorsDTO GetErrorsJSON(string errorMessages)
        {
            // The errors are line seperated... Getting each, performing trimming, checking if empty...
            // Thereafter, pushing it into the final list
            List<string> errorList = errorMessages.Split("\n")
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();
            return new OutgoingErrorsDTO
            {
                Errors = errorList
            };
        }

        // Used to send the final response to the user....
        public async Task SendResponse(
            HttpStatusCode statusCode,
            string message,
            HttpListenerResponse responseObj,
            bool isError=false)
        {
            string finalMessage = message;
            if (isError)
            {
                // if this message is classified as an error, splitting to list and then serializing them
                finalMessage = JsonSerializer.Serialize(GetErrorsJSON(message));
            }
            byte[] responseMessage = Encoding.UTF8.GetBytes(finalMessage);
            responseObj.ContentLength64 = responseMessage.Length;
            responseObj.StatusCode = (int)statusCode;
            await responseObj.OutputStream.WriteAsync(responseMessage, 0, responseMessage.Length);
        }

        public async Task<bool> ValidateObject<T>(T? incomingObject, HttpListenerResponse responseObj) where T: class
        {
            if (incomingObject == null)
            {
                return false;
            }
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(incomingObject);
            // evaluating all the properties of object and identifying if any errors
            bool isValid = Validator.TryValidateObject(incomingObject, validationContext, validationResults, true);
            if (!isValid)
            {
                // grouping all validation errors together line-by-line
                string validationErrors = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                await this.SendResponse(HttpStatusCode.BadRequest, validationErrors, responseObj, true);
                return false;
            }
            return true;
        }

    }
}
