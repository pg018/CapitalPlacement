using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO.Stages;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO.StagesType
{
    public class WorkFlowStageDTO
    {
        [DefaultStringValidator("Stage Name")]
        public string? StageName { get; set; }
        [ObjectAttributeValidator("Stage Type")]
        public BaseWorkflowStageTypeDTO StageType { get; set; }
        [NonNullValidator("Hide From Candidate")]
        public bool? HideFromCandidate { get; set; }
    }
}
