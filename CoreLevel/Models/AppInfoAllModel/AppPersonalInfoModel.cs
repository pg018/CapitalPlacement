using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo;

namespace CapitalPlacement.CoreLevel.Models.AppInfoAllModel
{
    // all the properties from frontend there... Adding First name, last name and email properties also
    public class AppPersonalInfoModel: AppPersonalInfoDTO
    {
        public AppPersonalInfoItemDTO FirstName {  get; set; }
        public AppPersonalInfoItemDTO LastName {  get; set; }
        public AppPersonalInfoItemDTO Email {  get; set; }
    }
}
