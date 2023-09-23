using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo;

namespace CapitalPlacement.CoreLevel.Models.AppInfoAllModel
{
    public class AppInfoModel : ProgramDetailsModel
    {
        public string CoverImage { get; set; }

        public AppPersonalInfoModel PersonalInfo { get; set; }

        public AppProfileInfoDTO ProfileInfo { get; set; }

        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
    }
}
