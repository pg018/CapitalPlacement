
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Net;

namespace CapitalPlacement.Controllers
{
    public class WorkflowController
    {
        private readonly ICommonService _commonService;
        private readonly IWorkflowService _workflowService;
        
        public WorkflowController(ICommonService commonService, IWorkflowService workflowService)
        {
            _commonService = commonService;
            _workflowService = workflowService;
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

        private async Task HandleGet(HttpListenerRequest request, HttpListenerResponse response) { }

        private async Task HandlePut(HttpListenerRequest request, HttpListenerResponse response)
        {
            using var reader = new StreamReader(request.InputStream);
            var requestBody = await reader.ReadToEndAsync();
            Console.WriteLine($"{requestBody}");
            var finalIncomingObject = _workflowService.GetFinalIncomingDTO(requestBody);
            var isValid = await _commonService.ValidateObject<IncomingWorkflowDTO>(finalIncomingObject, response);
            if (!isValid || finalIncomingObject == null)
            {
                return;
            }
        }
    }
}
