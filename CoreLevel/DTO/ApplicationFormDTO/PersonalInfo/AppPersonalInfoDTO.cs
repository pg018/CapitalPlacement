using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo
{
    public class AppPersonalInfoDTO
    {
        public AppPersonalInfoItemDTO PhoneNumber { get; set; }
        public AppPersonalInfoItemDTO Nationality { get; set; }
        public AppPersonalInfoItemDTO CurrentResidence { get; set; }
        public AppPersonalInfoItemDTO IdNumber { get; set; }
        public AppPersonalInfoItemDTO DateOfBirth { get; set; }
        public AppPersonalInfoItemDTO Gender { get; set; }
        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; }
    }
}
