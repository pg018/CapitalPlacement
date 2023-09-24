using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.Models.WorkflowAllModel;
using CapitalPlacement.CoreLevel.Services;
using System.Text.Json;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class WorkflowExtensions
    {
        public static string ConvertDTOToModelString(this IncomingWorkflowDTO dtoObject, string dbString)
        {
            JsonDocument document = JsonDocument.Parse(dbString);
            // getting the list of workflow stages
            var workflowStagesElement = document.RootElement.GetProperty("WorkflowStages").ToString();
            var workflowList = new List<object>();
            if (workflowStagesElement != string.Empty)
            {
                foreach (var element in JsonDocument.Parse(workflowStagesElement).RootElement.EnumerateArray())
                {
                    // parsing them and adding in the list
                    workflowList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject(element.ToString()));
                }
            }
            WorkflowSingleStageModel newStage = new()
            {
                StageName = dtoObject.StageItem.StageName,
                StageType = dtoObject.StageItem.StageType,
                HideFromCandidate = dtoObject.StageItem.HideFromCandidate,
                Order = workflowList.Count + 1,
            };
            // adding the new stage
            workflowList.Add(newStage);
            var serializedWorkflowList = Newtonsoft.Json.JsonConvert.SerializeObject(workflowList);
            // updating the existing workflowStages list with new list
            var finalDoc = JsonService.UpdateExistingProperty(
                document, "WorkflowStages", serializedWorkflowList);
            return JsonService.SerializeJsonDocument(finalDoc);
        }

        public static OutgoingWorkflowDTO GetOutgoingWorkflowFromModel(string cosmosString)
        {
            JsonDocument document = JsonDocument.Parse(cosmosString);
            // getting the id property
            var id = document.RootElement.GetProperty("id").GetString();
            JsonElement workflowStagesListElement;
            // getting the workflow stages list
            document.RootElement.TryGetProperty("WorkflowStages", out workflowStagesListElement);
            string workflowElementString = workflowStagesListElement.ToString();
            object? workflowList = null;
            if (workflowElementString != string.Empty)
            {
                // if workflow list exists in document we deserialize it
                workflowList = JsonSerializer.Deserialize<dynamic>(workflowElementString);
            }
            return new OutgoingWorkflowDTO
            {
                documentId = id,
                StageList = workflowList,
            };
        }

    }
}
