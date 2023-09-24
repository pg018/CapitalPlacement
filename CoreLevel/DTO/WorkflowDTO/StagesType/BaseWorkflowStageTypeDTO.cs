
using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO.Stages
{
    public class BaseWorkflowStageTypeDTO
    {
        [NonNullValidator("Stage Type Id")]
        public int StageTypeId { get; set; }
        
    }
}
