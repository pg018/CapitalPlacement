
using CapitalPlacement.CoreLevel.Enums;
using CapitalPlacement.CoreLevel.Models.WorkflowAllModel;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO
{
    public class OutgoingWorkflowDTO
    {
        public string documentId {  get; set; }
        public List<WorkflowStageMapping> WorkflowStageTypes { get; set; }

        // Directly the model object as order is also required to show in frontend
        public List<WorkflowSingleStageModel> StageList {  get; set; }
    }
}
