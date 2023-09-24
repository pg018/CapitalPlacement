
namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO
{
    public class OutgoingFormData
    {
        public string id {  get; set; }
        // sending the first name, last name and email also
        public object? PersonalInfo {  get; set; }
        public object? ProfileInfo {  get; set; }
        public string CoverImage {  get; set; }
        public object? AdditionalQuestions {  get; set; }
    }
}
