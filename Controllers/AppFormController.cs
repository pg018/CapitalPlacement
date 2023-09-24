
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.ModelExtensions.cs;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.ServiceContracts;
using CapitalPlacement.CoreLevel.Services;
using System.Net;
using System.Text.Json;
using System.Web;

namespace CapitalPlacement.Controllers
{
    public class AppFormController
    {
        private readonly ICommonService _commonService;
        private readonly ICosmosService<NewApplicationFormModel> _applicationCosmosService;
        private readonly IApplicationFormService _applicationFormService;

        public AppFormController(
            ICommonService commonService,
            ICosmosService<NewApplicationFormModel> applicationCosmosService,
            IApplicationFormService applicationFormService)
        {
            _commonService = commonService;
            _applicationFormService = applicationFormService;
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
                case "PUT":
                    await HandlePut(request, response);
                    break;
            }
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
            string? documentString = await _applicationCosmosService.ReadItemAsyncString(documentId);
            if (documentString == null)
            {
                // no such document exists
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found!", response, true);
                return;
            }
            Console.WriteLine("Document Exists in DB");
            // sending the data that is required for application form only
            var questionTypeList = _applicationFormService.GetQuestionTypesList();
            var finalDocument = AppInfoModelExtension.ConvertModelToOutgoingAppInfo(documentString);
            finalDocument.QuestionTypes = questionTypeList;

            var finalString = JsonSerializer.Serialize(finalDocument);
            await _commonService.SendResponse(HttpStatusCode.OK, finalString, response);
            await Task.CompletedTask;
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Running the Put Function");
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            Console.WriteLine(requestBody);
            var jsonObject = _applicationFormService.GetFinalIncomingDTO(requestBody);

            if (!await _commonService.ValidateObject<IncomingAppInfoDTO>(jsonObject, response) || jsonObject == null)
            {
                return;
            }
            Console.WriteLine("All Testing Passed");
            // retrieving the document if there in db
            string? documentString = await _applicationCosmosService.ReadItemAsyncString(jsonObject.id);
            if (documentString == null)
            {
                // no such document exists
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found!", response, true);
                return;
            }
            // database string document parsed to jsonDocument
            var jsonDoc = JsonDocument.Parse(documentString);
            // the incoming object is serialized to string
            var jsonObjectString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);
            // the properties are updated in the jsonDoc and finally returns an updated Json Document
            var finalJsonDoc = _applicationFormService.GetFinalModelDocument(jsonDoc, jsonObjectString);            
            // final JsonDocument serialized to string
            var finalDocString = JsonService.SerializeJsonDocument(finalJsonDoc);
            // replacing the document in the database with updated one
            await _applicationCosmosService.ReplaceItemAsyncString(finalDocString, jsonObject.id);
            await Task.CompletedTask;
        }
    }
}
