using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo
{
    public class AppPersonalInfoItemDTO
    {
        [Required(ErrorMessage = "Internal Property is Required")]
        public bool Internal { get; set; }
        [Required(ErrorMessage = "Hide Property is Required")]
        public bool Hide {  get; set; }
    }
}
