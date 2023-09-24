
using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo
{
    public class AppProfileInfoItemDTO
    {
        [NonNullValidator("Mandatory")]
        public bool? Mandatory { get; set; }

        [NonNullValidator("Hide")]
        public bool? Hide {  get; set; }
    }
}
