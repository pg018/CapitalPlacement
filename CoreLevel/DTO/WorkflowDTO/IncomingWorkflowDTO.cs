
using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO.StagesType;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO
{
    public class IncomingWorkflowDTO
    {
        [DefaultStringValidator("Document Id")]
        public string? id { get; set; }
        [ObjectAttributeValidator("Stage")]
        public WorkFlowStageDTO StageItem {  get; set; }
    }
}
