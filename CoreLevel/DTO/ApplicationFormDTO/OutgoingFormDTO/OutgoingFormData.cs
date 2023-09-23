using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO
{
    public class OutgoingFormData
    {
        public string id {  get; set; }
        // sending the first name, last name and email also
        public AppPersonalInfoModel PersonalInfo {  get; set; }
        public AppProfileInfoDTO ProfileInfo {  get; set; }
        public string CoverImage {  get; set; }
        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; }
    }
}
