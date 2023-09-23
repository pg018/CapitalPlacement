
namespace CapitalPlacement.CoreLevel.Enums
{
    public enum AppFormQuestionTypeEnum
    {
        Paragraph = 1,
        ShortAnswer = 2,
        YesNo = 3,
        Dropdown = 4,
        MultipleChoice = 5,
        Date = 6,
        Number = 7,
        FileUpload = 8, 
        VideoQuestion = 9,
    }

    public class QuestionTypeMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
