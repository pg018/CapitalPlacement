using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class AppInfoModelExtension
    {
        public static OutgoingAppInfoDTO ConvertModelToOutgoingAppInfo(
            string dbString)
        {
            JsonDocument document = JsonDocument.Parse(dbString);
            object? personalInfoObj = null, profileInfoObj = null, additionalQEl = new List<object>();
            string coverImageElementString = document.RootElement.GetProperty("CoverImage").ToString();
            string personalInfoElementString = document.RootElement.GetProperty("PersonalInfo").ToString();
            string profileInfoElementString = document.RootElement.GetProperty("ProfileInfo").ToString();
            string addQuestionsElementString = document.RootElement.GetProperty("AdditionalQuestions").ToString();
            if (personalInfoElementString != string.Empty)
            {
                personalInfoObj = JsonSerializer.Deserialize<dynamic>(personalInfoElementString);
            }
            if (personalInfoElementString != string.Empty)
            {
                profileInfoObj = JsonSerializer.Deserialize<dynamic>(profileInfoElementString);
            }
            if (addQuestionsElementString != string.Empty)
            {
                additionalQEl = JsonSerializer.Deserialize<dynamic>(addQuestionsElementString);
            }
            return new OutgoingAppInfoDTO
            {
                AppInfo = new OutgoingFormData
                {
                    id = document.RootElement.GetProperty("id").GetString(), // id exists by default
                    PersonalInfo = personalInfoObj,
                    ProfileInfo = profileInfoObj,
                    CoverImage = coverImageElementString,
                    AdditionalQuestions = additionalQEl,
                }
            };
        }
    }
}
