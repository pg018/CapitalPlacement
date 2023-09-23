using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;
using CapitalPlacement.CoreLevel.Models.WorkflowAllModel;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class WorkflowExtensions
    {
        public static WorkflowModel ConvertDTOToModel(this IncomingWorkflowDTO dtoObject, WorkflowModel prevWorkFlowModel)
        {
            var finalList = prevWorkFlowModel.WorkflowStages.ToList();
            WorkflowSingleStageModel newStage = new()
            {
                StageName = dtoObject.StageItem.StageName,
                StageType = dtoObject.StageItem.StageType,
                HideFromCandidate = dtoObject.StageItem.HideFromCandidate,
                Order = finalList.Count + 1,
            };
            finalList.Add(newStage);
            return new WorkflowModel()
            {
                WorkflowStages = finalList
            };
        }
    }
}
