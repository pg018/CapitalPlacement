using System.Net;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface ICommonService
    {
        public Task SendResponse(
            HttpStatusCode statusCode,
            string message,
            HttpListenerResponse response,
            bool isError = false);

        public Task<bool> ValidateObject<T>(T? incomingObject, HttpListenerResponse responseObj) where T : class;
    }
}
