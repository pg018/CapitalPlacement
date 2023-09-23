
using CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO;
using CapitalPlacement.CoreLevel.ModelExtensions.cs;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Net;
using System.Text.Json;
using System.Web;

namespace CapitalPlacement.Controllers
{
    public class ProgramDetailsController
    {
        private readonly ICommonService _commonService;
        private readonly ICosmosService<NewApplicationFormModel> _applicationCosmosService;

        public ProgramDetailsController(
            ICommonService commonService,
            ICosmosService<NewApplicationFormModel> applicationCosmosService)
        {
            _commonService = commonService;
            _applicationCosmosService = applicationCosmosService;
        }

        public async Task HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            var httpMethod = request.HttpMethod.ToUpper();
            Console.WriteLine($"Method {httpMethod}");
            switch (httpMethod)
            {
                case "GET":
                    await HandleGet(request, response);
                    break;
                case "POST":
                    await HandlePost(request, response);
                    break;
                case "PUT":
                    await HandlePut(request, response);
                    break;
            }
        }

        private async Task<bool> ValidateProgramApplicationDates(IncomingProgramDetailsDTO jsonObject, HttpListenerResponse response)
        {
            // used to validate the application close and program start dates
            if (jsonObject.AdditionalProgramInfo.ProgramStart != null
                && jsonObject.AdditionalProgramInfo.ApplicationClose > jsonObject.AdditionalProgramInfo.ProgramStart)
            {
                // if program start is entered, then it must be after application closes....
                await _commonService.SendResponse(
                    HttpStatusCode.BadRequest,
                    "Program Start Must be after Application Close",
                    response,
                    true);
                return false;
            }
            return true;
        }

        private async Task HandleGet(HttpListenerRequest request, HttpListenerResponse response) 
        {
            Console.WriteLine("Running the GET function");
            var queryParams = HttpUtility.ParseQueryString(request.Url.Query);
            // extracting the document id from query
            var documentId = queryParams["id"];
            Console.WriteLine($"Document ID => {documentId}");
            if (string.IsNullOrEmpty(documentId))
            {
                await _commonService.SendResponse(HttpStatusCode.BadRequest, "Invalid Id!", response, true);
                return;
            }
            var retrievedDocument = await _applicationCosmosService.GetByIdAsync(documentId);
            if (retrievedDocument == null) 
            {
                // no such document exists
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found!", response, true);
                return;
            }
            // here the outgoing document is same as the model so returning the model itself...
            var finalString = JsonSerializer.Serialize(retrievedDocument.GetOutgoingProgramDetailsFromModel());
            await _commonService.SendResponse(HttpStatusCode.OK, finalString, response);
            await Task.CompletedTask;
        }

        private async Task HandlePost(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Running the Post Function");
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            // deserializing and converting to json object
            var jsonObject = JsonSerializer.Deserialize<IncomingProgramDetailsDTO>(requestBody);
            if (!await _commonService.ValidateObject<IncomingProgramDetailsDTO>(jsonObject, response) || jsonObject == null)
            {
                return;
            }
            if (!await ValidateProgramApplicationDates(jsonObject, response))
            {
                return;
            }
            Console.WriteLine("All Testing Passed");
            var finalDocument = ProgramDetailsModelExtension.ConvertIncomingDTOToModel(jsonObject, true);
            Console.WriteLine(finalDocument.id);
            await _applicationCosmosService.CreateItemAsync(finalDocument);
            response.StatusCode = (int)HttpStatusCode.Created;
            await Task.CompletedTask;
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Running the Put Function");
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            var jsonObject = JsonSerializer.Deserialize<IncomingProgramDetailsDTO>(requestBody);
            if (!await _commonService.ValidateObject(jsonObject, response) || jsonObject == null) { return; }
            if (!await ValidateProgramApplicationDates(jsonObject, response))
            {
                return;
            }
            if (jsonObject.id == null || string.IsNullOrEmpty(jsonObject.id)) 
            {
                // check if the id is entered which is used to identify the document
                await _commonService.SendResponse(HttpStatusCode.BadRequest, "Id is Invalid", response, true);
                return; 
            }
            Console.WriteLine("All Test Cases Passed");
            var dbDocument = await _applicationCosmosService.GetByIdAsync(jsonObject.id);
            if (dbDocument == null)
            {
                await _commonService.SendResponse(HttpStatusCode.BadRequest, "Resource Not Found", response, true);
                return;
            }
            var finalDocument = ProgramDetailsModelExtension.ConvertIncomingDTOToModel(jsonObject,applicationModel: dbDocument);
            Console.WriteLine("Replacing the final document");
            await _applicationCosmosService.ReplaceItemAsync(finalDocument);
            response.StatusCode = (int)HttpStatusCode.OK;
            await Task.CompletedTask;
        }
    }
}
