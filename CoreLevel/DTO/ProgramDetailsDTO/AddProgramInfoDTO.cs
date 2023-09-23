
using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO
{
    public class AddProgramInfoDTO
    {
        [DefaultStringValidator("Program Type")]
        public string? ProgramType { get; set; }
        public DateTime? ProgramStart {  get; set; }
        [StartEndDateValidator("Application Start", "Application Close", "ApplicationClose")]
        public DateTime ApplicationOpen {  get; set; }
        public DateTime ApplicationClose { get; set; }
        public string? Duration { get; set; } = string.Empty;

        [LocationOrRemoteValidator("isRemote", "Fully Remote")]
        public string? Location { get; set; }
        public bool isRemote { get; set; }
        public string? MinQualifications {  get; set; }
        public int? MaxApplications { get; set; } = null;
    }
}
