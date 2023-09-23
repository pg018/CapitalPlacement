
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface IWorkflowService
    {
        public IncomingWorkflowDTO? GetFinalIncomingDTO(string requestBody);
    }
}
