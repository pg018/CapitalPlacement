using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.ProfileInfo
{
    public class AppProfileInfoDTO
    {
        public AppProfileInfoItemDTO Education { get; set; }
        public AppProfileInfoItemDTO Experience { get; set; }
        public AppProfileInfoItemDTO Resume {  get; set; }

        public List<BaseQuestionDTO> AdditionalQuestions {  get; set; }
    }
}
