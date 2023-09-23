using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo
{
    public class AppPersonalInfoDTO
    {
        [ObjectAttributeValidator("Phone Number")]
        public AppPersonalInfoItemDTO PhoneNumber { get; set; }

        [ObjectAttributeValidator("Nationality")]
        public AppPersonalInfoItemDTO Nationality { get; set; }

        [ObjectAttributeValidator("Current Residence")]
        public AppPersonalInfoItemDTO CurrentResidence { get; set; }

        [ObjectAttributeValidator("ID Number")]
        public AppPersonalInfoItemDTO IdNumber { get; set; }

        [ObjectAttributeValidator("Date Of Birth")]
        public AppPersonalInfoItemDTO DateOfBirth { get; set; }

        [ObjectAttributeValidator("Gender")]
        public AppPersonalInfoItemDTO Gender { get; set; }

        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; }
    }
}
