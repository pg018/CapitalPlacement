
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.Enums;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface IApplicationFormService
    {
        public IncomingAppInfoDTO? GetFinalIncomingDTO(string requestBody);
        public List<QuestionTypeMapping> GetQuestionTypesList();
        public List<BaseQuestionDTO> GetFinalAdditionalQuestionsList(string additionalQuestionsJson);
        public JsonDocument GetFinalModelDocument(JsonDocument dbDocument, string jsonObjectString);
    }
}
