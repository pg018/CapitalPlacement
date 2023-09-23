using CapitalPlacement.CoreLevel.CustomValidators;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo
{
    public class AppProfileInfoDTO
    {
        [ObjectAttributeValidator("Education")]
        public AppProfileInfoItemDTO Education { get; set; }

        [ObjectAttributeValidator("Experience")]
        public AppProfileInfoItemDTO Experience { get; set; }

        [ObjectAttributeValidator("Resume")]
        public AppProfileInfoItemDTO Resume {  get; set; }

        [ObjectAttributeValidator("Additional Questions", true)]
        public List<BaseQuestionDTO> AdditionalQuestions { get; set; } = new List<BaseQuestionDTO>();
    }
}
