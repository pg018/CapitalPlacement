
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.OutgoingFormDTO;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo;
using CapitalPlacement.CoreLevel.Enums;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class AppInfoModelExtension
    {
        public static NewApplicationFormModel ConvertDTOToModel(
            this IncomingAppInfoDTO dtoObject,
            NewApplicationFormModel applicationModel)
        {
            AppPersonalInfoModel finalPersonalObj = new AppPersonalInfoModel
            {
                FirstName = new AppPersonalInfoItemDTO { Internal = true, Hide = false },
                LastName = new AppPersonalInfoItemDTO { Internal = true, Hide = false },
                Email = new AppPersonalInfoItemDTO { Internal = true, Hide = false },
                PhoneNumber = dtoObject.PersonalInfo.PhoneNumber,
                Nationality = dtoObject.PersonalInfo.Nationality,
                CurrentResidence = dtoObject.PersonalInfo.CurrentResidence,
                IdNumber = dtoObject.PersonalInfo.IdNumber,
                DateOfBirth = dtoObject.PersonalInfo.DateOfBirth,
                Gender = dtoObject.PersonalInfo.Gender,
                AdditionalQuestions = dtoObject.PersonalInfo.AdditionalQuestions,
            };
            applicationModel.CoverImage = dtoObject.CoverImage;
            applicationModel.PersonalInfo = finalPersonalObj;
            applicationModel.ProfileInfo = dtoObject.ProfileInfo;
            applicationModel.AdditionalQuestions = dtoObject.AdditionalQuestions;

            return applicationModel;
        }

        public static OutgoingAppInfoDTO ConvertModelToOutgoingAppInfo(
            this NewApplicationFormModel modelObj,
            List<QuestionTypeMapping> questionTypes)
        {
            
            return new OutgoingAppInfoDTO
            {
                QuestionTypes = questionTypes,
                AppInfo = new OutgoingFormData
                {
                    id = modelObj.id,
                    PersonalInfo = modelObj.PersonalInfo,
                    ProfileInfo = modelObj.ProfileInfo,
                    CoverImage = modelObj.CoverImage,
                    AdditionalQuestions = modelObj.AdditionalQuestions,
                }
            };
        }
    }
}
