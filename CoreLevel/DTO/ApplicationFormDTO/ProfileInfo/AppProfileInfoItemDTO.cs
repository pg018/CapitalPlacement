
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo
{
    public class AppProfileInfoItemDTO
    {
        [Required(ErrorMessage = "Mandatory Property is Required")]
        public bool Mandatory { get; set; }

        [Required(ErrorMessage = "Hide Property is Required")]
        public bool Hide {  get; set; }
    }
}
