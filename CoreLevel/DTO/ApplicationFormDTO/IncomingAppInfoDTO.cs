
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO
{
    public class IncomingAppInfoDTO
    {
        public string id {  get; set; }
        public string CoverImage { get; set; }
        public AppPersonalInfoDTO PersonalInfo {  get; set; }
        public AppProfileInfoDTO ProfileInfo { get; set; }
        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; }
    }
}
