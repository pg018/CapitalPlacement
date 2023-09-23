using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO.Stages;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO.StagesType.VideoInterviewStage
{
    // this class is the structure if question interview is selected 
    public class VideoInterviewStageDTO : BaseWorkflowStageTypeDTO
    {
        [ObjectAttributeValidator("Video Questions")]
        public List<VideoInterviewQuestionDTO> VideoQuestionsList { get; set; }
    }
}
