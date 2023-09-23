
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO;
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.Enums;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface IApplicationFormService
    {
        public IncomingAppInfoDTO? GetFinalIncomingDTO(string requestBody);
        public List<QuestionTypeMapping> GetQuestionTypesList();
    }
}
