
namespace CapitalPlacement.CoreLevel.Enums
{
    public enum WorkflowStageEnum
    {
        Shortlisting = 1,
        VideoInterview = 2,
        Placement = 3,
    }

    public class WorkflowStageMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
