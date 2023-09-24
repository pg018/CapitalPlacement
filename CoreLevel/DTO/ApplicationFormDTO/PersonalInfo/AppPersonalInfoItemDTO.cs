using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo
{
    public class AppPersonalInfoItemDTO
    {
        [NonNullValidator("Internal")]
        public bool? Internal { get; set; }
        [NonNullValidator("Hide")]
        public bool? Hide {  get; set; }
    }
}
