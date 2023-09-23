
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo;
using CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;
using CapitalPlacement.CoreLevel.Models.WorkflowAllModel;
using CapitalPlacement.Infrastructure;

namespace CapitalPlacement.CoreLevel.Models
{
    public class NewApplicationFormModel: CosmosDocument
    {
        public ProgramInfoDTO ProgramInfo { get; set; }
        public AddProgramInfoDTO AdditionalProgramInfo { get; set; }
        public string CoverImage { get; set; }

        public AppPersonalInfoModel PersonalInfo { get; set; }

        public AppProfileInfoDTO ProfileInfo { get; set; }

        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
        public List<WorkflowSingleStageModel> WorkflowStages { get; set; }
    }
}
