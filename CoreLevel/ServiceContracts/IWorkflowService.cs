
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.Enums;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface IWorkflowService
    {
        public IncomingWorkflowDTO? GetFinalIncomingDTO(string requestBody);

        public List<WorkflowStageMapping> GetQuestionTypesList();

    }
}
