
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.ModelExtensions.cs;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;
using CapitalPlacement.CoreLevel.ServiceContracts;
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
            var retrievedDocument = await _applicationCosmosService.GetByIdAsync(documentId);
            if (retrievedDocument == null)
            {
                // no such document exists
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found!", response, true);
                return;
            }

            // sending the data that is required for application form only
            var questionTypeList = _applicationFormService.GetQuestionTypesList();
            var finalDocument = AppInfoModelExtension.ConvertModelToOutgoingAppInfo(retrievedDocument, questionTypeList);

            // here the outgoing document is same as the model so returning the model itself...
            var finalString = JsonSerializer.Serialize(finalDocument);
            await _commonService.SendResponse(HttpStatusCode.OK, finalString, response);
            await Task.CompletedTask;
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine("Running the Post Function");
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
            var dbDocument = await _applicationCosmosService.GetByIdAsync(jsonObject.id);
            if (dbDocument == null)
            {
                // document does not exist... Someone must have tampered with id in frontend
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found", response, true);
                return;
            }
            var finalDocument = AppInfoModelExtension.ConvertDTOToModel(jsonObject, dbDocument);
            await _applicationCosmosService.ReplaceItemAsync(finalDocument);
            await Task.CompletedTask;
        }
    }
}
