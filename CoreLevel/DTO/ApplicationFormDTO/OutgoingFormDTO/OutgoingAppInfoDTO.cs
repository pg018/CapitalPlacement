using CapitalPlacement.CoreLevel.Enums;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO
{
    public class OutgoingAppInfoDTO
    {
        // sending the data for the application form
        public List<QuestionTypeMapping> QuestionTypes { get; set; }
        public OutgoingFormData AppInfo { get; set; }
    }
}
