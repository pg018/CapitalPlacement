using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.DTO.WorkflowDTO.StagesType.VideoInterviewStage;
using CapitalPlacement.CoreLevel.Enums;
using CapitalPlacement.CoreLevel.ServiceContracts;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.Services
{
    public class WorkflowService: IWorkflowService
    {
        public List<WorkflowStageMapping> GetQuestionTypesList()
        {
            List<WorkflowStageMapping> workflowStagesList = new List<WorkflowStageMapping>();
            foreach (WorkflowStageEnum type in Enum.GetValues(typeof(WorkflowStageEnum)))
            {
                workflowStagesList.Add(new WorkflowStageMapping
                {
                    Id = (int)type,
                    Name = type.ToString()
                });
            }
            return workflowStagesList;
        }

        public IncomingWorkflowDTO? GetFinalIncomingDTO(string requestBody)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(requestBody);
            var stageTypeObj = jsonDocument
                .RootElement.GetProperty("StageItem")
                .GetProperty("StageType");
            var stageTypeId = stageTypeObj.GetProperty("StageTypeId").GetInt32();
            var finalObject = JsonSerializer.Deserialize<IncomingWorkflowDTO>(requestBody);
            switch (stageTypeId)
            {
                case 1:
                case 3:
                    break;
                case 2:
                    var questionsListString = stageTypeObj.GetProperty("VideoQuestionsList").ToString();
                    var finalQuestionsList = GetVideoInterviewQuestionsList(questionsListString);
                    finalObject!.StageItem.StageType = new VideoInterviewStageDTO() 
                    { 
                        StageTypeId = stageTypeId,
                        VideoQuestionsList=finalQuestionsList 
                    };
                    break;
            }
            return finalObject;
        }

        private List<VideoInterviewQuestionDTO> GetVideoInterviewQuestionsList(string videoQuestionsListString)
        {
            List<VideoInterviewQuestionDTO> finalQuestionsList = new();
            foreach (var question in JsonDocument.Parse(videoQuestionsListString).RootElement.EnumerateArray())
            {
                VideoInterviewQuestionDTO questionObj = new()
                {
                    Question = question.GetProperty("Question").GetString(),
                    AdditionalInformation = question.GetProperty("AdditionalInformation").GetString(),
                    MaxDuration = question.GetProperty("MaxDuration").GetInt32(),
                    isSeconds = question.GetProperty("isSeconds").GetBoolean(),
                    DeadlineDays = question.GetProperty("DeadlineDays").GetInt32()
                };
                finalQuestionsList.Add(questionObj);
            }
            return finalQuestionsList;
        }
    }
}
