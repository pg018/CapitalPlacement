using CapitalPlacement.CoreLevel.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.CoreLevel.DTO.WorkflowDTO.StagesType.VideoInterviewStage
{
    // This class is the structure of a question in video interview
    public class VideoInterviewQuestionDTO
    {
        [DefaultStringValidator("Question in Video Interview")]
        public string? Question {  get; set; }

        [DefaultStringValidator("Additional Information in Video Interview")]
        public string? AdditionalInformation { get; set; }
        [NonNullValidator("Max Duration in Video Interview")]
        public int MaxDuration { get; set; }
        [NonNullValidator("Sec/Min in Video Interview")]
        public bool isSeconds {  get; set; }
        [Required(ErrorMessage = "Deadline days in Video Interview")]
        [NonNullValidator("Deadline Days in Video Interview")]
        public int DeadlineDays {  get; set; }
    }
}
