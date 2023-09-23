
using CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions;
using CapitalPlacement.CoreLevel.Enums;

namespace CapitalPlacement.CoreLevel.ServiceContracts
{
    public interface IApplicationFormService
    {
        public List<BaseQuestionDTO> GetFinalAdditionalQuestionsList(string additionalQuestionsJson);
        public List<QuestionTypeMapping> GetQuestionTypesList();
    }
}
