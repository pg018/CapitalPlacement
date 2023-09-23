
namespace CapitalPlacement.CoreLevel.DTO.ApplicationFormDTO.AdditionalQuestions
{
    public class DerivedQuestionsDTO
    {
        public class ParagraphQuestionDTO : BaseQuestionDTO 
        {
            public override string ToString()
            {
                return $"Question => {Question}\n" + $"Type => {Type}\n";
            }
        }

        public class ShortAnswerQuestionDTO: BaseQuestionDTO { }

        public class YesNoQuestionDTO : BaseQuestionDTO
        {
            public bool DisqualifyIfno { get; set; }
        }

        public class DropdownQuestionDTO : BaseQuestionDTO
        {
            public List<string> Options { get; set; }
            public bool EnableOther { get; set; }
        }

        public class MultipleChoiceQuestionDTO : BaseQuestionDTO
        {
            public List<string> Options { get; set; }
            public bool EnableOther { get; set; }
            public int MaxChoicesAllowed {  get; set; }
        }

        public class DateQuestionDTO: BaseQuestionDTO { }

        public class NumberQuestionDTO: BaseQuestionDTO { }

        public class FileUploadQuestionDTO: BaseQuestionDTO { }
        public class VideoQuestionDTO: BaseQuestionDTO { }
    }
}
