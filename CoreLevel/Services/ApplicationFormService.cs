
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
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
                    case 6: // date
                        DateQuestionDTO dateQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(dateQuestionDTO);
                        break;
                    case 7: // number
                        NumberQuestionDTO numberQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(numberQuestionDTO);
                        break;
                    case 8: // FileUpload
                        FileUploadQuestionDTO fileUploadQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(fileUploadQuestionDTO);
                        break;
                    case 9://Video Question
                        VideoQuestionDTO videoQuestionDTO = new()
                        {
                            Question = element.GetProperty("Question").GetString(),
                            Type = type,
                        };
                        finalList.Add(videoQuestionDTO);
                        break;
                    default:
                        break;
                }
            }

            return finalList;
        }
    }
}
