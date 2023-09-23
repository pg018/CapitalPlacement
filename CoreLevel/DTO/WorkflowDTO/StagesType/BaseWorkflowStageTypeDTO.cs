
using CapitalPlacement.CoreLevel.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO.Stages
{
    public class BaseWorkflowStageTypeDTO
    {
        [NonNullValidator("Stage Type Id")]
        public int StageTypeId { get; set; }
        
    }
}
