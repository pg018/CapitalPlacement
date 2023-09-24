
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Net;
using System.Web;

namespace CapitalPlacement.Controllers
{


    public class PreviewController
    {
        private readonly ICosmosService<NewApplicationFormModel> _cosmosService;
        private readonly ICommonService _commonService;
        
        public PreviewController(ICosmosService<NewApplicationFormModel> cosmosService, ICommonService commonService)
        {
            _cosmosService = cosmosService;
            _commonService = commonService;
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
            string? documentString = await _cosmosService.ReadItemAsyncString(documentId);
            if (documentString == null)
            {
                // no such document exists
                await _commonService.SendResponse(HttpStatusCode.NotFound, "Resource Not Found!", response, true);
                return;
            }
            Console.WriteLine("Document Exists in DB");
            await _commonService.SendResponse(HttpStatusCode.OK, documentString, response);
        }
    }
}
