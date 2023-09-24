
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.ModelExtensions.cs;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Net;
using System.Text.Json;
using System.Web;

namespace CapitalPlacement.Controllers
{
    public class WorkflowController
    {
        private readonly ICommonService _commonService;
        private readonly IWorkflowService _workflowService;
        private readonly ICosmosService<NewApplicationFormModel> _applicationCosmosService;
        
        public WorkflowController(
            ICommonService commonService,
            IWorkflowService workflowService,
            ICosmosService<NewApplicationFormModel> applicationCosmosService)
        {
            _commonService = commonService;
            _workflowService = workflowService;
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
            var workflowStagesTypes = _workflowService.GetQuestionTypesList();
            // getting the final document which contains the required properties only
            var finalDocument = WorkflowExtensions.GetOutgoingWorkflowFromModel(documentString);
            finalDocument.WorkflowStageTypes = workflowStagesTypes;
            // serializing to string
            var finalString = JsonSerializer.Serialize(finalDocument);
            await _commonService.SendResponse(HttpStatusCode.OK, finalString, response, false);
        }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            var finalIncomingObject = _workflowService.GetFinalIncomingDTO(requestBody);
            if (!await _commonService.ValidateObject<IncomingWorkflowDTO>(finalIncomingObject, response)
                || finalIncomingObject == null
            )
            {
                return;
            }
            // checking and getting the document for the given document id
            var documentString = await _applicationCosmosService.ReadItemAsyncString(finalIncomingObject.id);
            if (documentString == null)
            {
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found", response, true);
                return;
            }
            // documentString is updated with the properties coming from finalIncomingObject and gives out the finalDocument
            var finalDocument = finalIncomingObject.ConvertDTOToModelString(documentString);
            // replacing the document in database
            await _applicationCosmosService.ReplaceItemAsyncString(finalDocument, finalIncomingObject.id);
            await Task.CompletedTask;
        }
    }
}
