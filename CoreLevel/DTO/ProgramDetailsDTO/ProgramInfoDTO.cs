using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO
{
    public class ProgramInfoDTO
    {
        [DefaultStringValidator("Program Title")]
        public string? ProgramTitle { get; set; }
        public string? ProgramSummary { get; set; }
        [DefaultStringValidator("Program Description")]
        public string? ProgramDescription { get; set; }
        public List<string> KeySkills { get; set; } = new List<string>();
        public string? ProgramBenifits { get; set; }
        public string? ApplicationCriteria { get; set; }
    }
}
