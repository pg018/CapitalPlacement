
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.PersonalInfo;
using CapitalPlacement.CoreLevel.Enums;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Text.Json;
using static CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions.DerivedQuestionsDTO;

namespace CapitalPlacement.CoreLevel.Services
{
    public class ApplicationFormService: IApplicationFormService
    {
        public List<QuestionTypeMapping> GetQuestionTypesList()
        {
            List<QuestionTypeMapping> questionTypes = new List<QuestionTypeMapping>();
            foreach (AppFormQuestionTypeEnum type in Enum.GetValues(typeof(AppFormQuestionTypeEnum)))
            {
                questionTypes.Add(new QuestionTypeMapping
                {
                    Id = (int)type,
                    Name = type.ToString()
                });
            }
            return questionTypes;
        }

        public IncomingAppInfoDTO? GetFinalIncomingDTO(string requestBody)
        {
            /*
             Initially when object is parsed from string, the properties of various questions are lost...
             In order to retrieve those properties, we extract them from string and then add it to the final
             object that is validated
             */
            JsonDocument jsonDocument = JsonDocument.Parse(requestBody);
            JsonElement additionalQuestionsElementPersonal,
                additionalQuestionsElementProfile,
                additionalQuestionsElementSolo;
            // getting the additional questions property from personal info
            jsonDocument.RootElement
                .GetProperty("PersonalInfo")
                .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementPersonal);
            // getting the additional questions property from profile info
            jsonDocument.RootElement
                .GetProperty("ProfileInfo")
                .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementProfile);
            // getting the additional questions property from main info
            jsonDocument.RootElement
                .TryGetProperty("AdditionalQuestions", out additionalQuestionsElementSolo);
            var additionalQuestionsPersonalString = additionalQuestionsElementPersonal.ToString();
            var additionalQuestionsProfileString = additionalQuestionsElementProfile.ToString();
            var additionalQuestionsSoloString = additionalQuestionsElementSolo.ToString();
            // deserializing to json object
            var jsonObject = JsonSerializer.Deserialize<IncomingAppInfoDTO>(requestBody);

            // checking if not empty string to evaluate that object exists
            if (additionalQuestionsPersonalString != string.Empty)
            {
                var personalFinalList = GetFinalAdditionalQuestionsList(additionalQuestionsPersonalString);
                jsonObject!.PersonalInfo!.AdditionalQuestions = personalFinalList;
            }
            if (additionalQuestionsProfileString != string.Empty)
            {
                var profileFinalList = GetFinalAdditionalQuestionsList(additionalQuestionsProfileString);
                jsonObject!.ProfileInfo!.AdditionalQuestions = profileFinalList;
            }
            if (additionalQuestionsSoloString != string.Empty)
            {
                var soloFinalList = GetFinalAdditionalQuestionsList(additionalQuestionsSoloString);
                jsonObject!.AdditionalQuestions = soloFinalList;
            }

            return jsonObject;
        }

        public List<BaseQuestionDTO> GetFinalAdditionalQuestionsList(string additionalQuestionsJson)
        {
            List<BaseQuestionDTO> finalList = new();
            Console.WriteLine("Starting the parsing of json string to get the questions");
            foreach(var element in JsonDocument.Parse(additionalQuestionsJson).RootElement.EnumerateArray())
            {
                // here directly getting property. If does not exist, directly throws error
                int type = element.GetProperty("Type").GetInt32();
                switch (type)
                {
                    case 1: // Paragraph
                    case 2: // shortanswer
                    case 6: // datetime => Not using the object because the requirement for now is same. Can use the object in future
                    case 7: // number
                    case 8: // file upload
                    case 9: // video question
                        BaseQuestionDTO commonQuestionsDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(commonQuestionsDTO);
                        break;
                    case 3: // YesNo
                        YesNoQuestionDTO yesNoQuestion = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            DisqualifyIfno = element.GetProperty("DisqualifyIfno").GetBoolean(),
                        };
                        finalList.Add(yesNoQuestion);
                        break;
                    case 4: //Dropdown
                        DropdownQuestionDTO dropdownQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            EnableOther = element.GetProperty("EnableOther").GetBoolean(),
                            Options = element.GetProperty("Options").EnumerateArray().Select(option => option.GetString()).ToList(),
                        };
                        finalList.Add(dropdownQuestionDTO);
                        break;
                    case 5: // Multiple Choice
                        MultipleChoiceQuestionDTO multipleChoiceQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                            EnableOther = element.GetProperty("EnableOther").GetBoolean(),
                            Options = element.GetProperty("Options").EnumerateArray().Select(option => option.GetString()).ToList(),
                            MaxChoicesAllowed = element.GetProperty("MaxChoicesAllowed").GetInt32(),
                        };
                        finalList.Add(multipleChoiceQuestionDTO);
                        break;
                    default:
                        break;
                }
            }
            return finalList;
        }

        public JsonDocument GetFinalModelDocument(JsonDocument dbDocument, string jsonObjectString)
        {
            AppPersonalInfoItemDTO FirstName = new()
            {
                Internal = false,
                Hide = false,
            };
            AppPersonalInfoItemDTO LastName = new()
            {
                Internal = false,
                Hide = false,
            };
            AppPersonalInfoItemDTO Email = new()
            {
                Internal = false,
                Hide = false,
            };
            var newPropertiesJsonObj = new {
                FirstName,
                LastName,
                Email,
            };
            string newPropertiesJsonString = JsonSerializer.Serialize(newPropertiesJsonObj);
            var updatedJsonDoc = JsonService.UpdateProperties(dbDocument,jsonObjectString);
            return JsonService.AddPropertiesToJsonDocument(updatedJsonDoc, "PersonalInfo", newPropertiesJsonString);
        }
    }
}
