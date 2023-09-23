
using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO
{
    public class IncomingAppInfoDTO
    {
        [DefaultStringValidator("Id")]
        public string id {  get; set; }

        [DefaultStringValidator("Cover Image")]
        public string CoverImage { get; set; }

        [ObjectAttributeValidator("Personal Information")]
        public AppPersonalInfoDTO PersonalInfo {  get; set; }

        [ObjectAttributeValidator("Profile Information")]
        public AppProfileInfoDTO ProfileInfo { get; set; }

        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
    }
}
