using CapitalPlacement.CoreLevel.Enums;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;

namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO
{
    public class OutgoingAppInfoDTO
    {
        // sending the data for the application form
        public List<QuestionTypeMapping> QuestionTypes { get; set; }
        public OutgoingFormData AppInfo { get; set; }
    }
}
