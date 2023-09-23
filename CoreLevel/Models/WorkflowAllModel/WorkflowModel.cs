using CapitalPlacement.CoreLevel.Models.AppInfoAllModel;

namespace CapitalPlacement.CoreLevel.Models.WorkflowAllModel
{
    // This is the final object to database object
    public class WorkflowModel : AppInfoModel
    {
        public List<WorkflowSingleStageModel> WorkflowStages { get; set; }
    }
}
