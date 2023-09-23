using CapitalPlacement.CoreLevel.DTO.WorkflowDTO;
using CapitalPlacement.CoreLevel.Models;
using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;
using CapitalPlacement.CoreLevel.Models.WorkflowAllModel;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class WorkflowExtensions
    {
        public static NewApplicationFormModel ConvertDTOToModel(this IncomingWorkflowDTO dtoObject, NewApplicationFormModel prevWorkFlowModel)
        {
            var finalList = prevWorkFlowModel.WorkflowStages;
            var listCount = 0;
            if (finalList != null)
            {
                listCount = finalList.Count;
            } else
            {
                finalList = new List<WorkflowSingleStageModel>();
            }
            WorkflowSingleStageModel newStage = new()
            {
                StageName = dtoObject.StageItem.StageName,
                StageType = dtoObject.StageItem.StageType,
                HideFromCandidate = dtoObject.StageItem.HideFromCandidate,
                Order = listCount + 1,
            };
            finalList.Add(newStage);
            prevWorkFlowModel.WorkflowStages = finalList;
            return prevWorkFlowModel;
        }

        public static OutgoingWorkflowDTO GetOutgoingWorkflowFromModel(this NewApplicationFormModel modelObj)
        {
            return new OutgoingWorkflowDTO
            {
                documentId = modelObj.id,
                StageList = modelObj.WorkflowStages
            };
        }
    }
}
