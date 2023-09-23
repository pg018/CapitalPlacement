
using CapitalPlacement.CoreLevel.CustomValidators;

namespace CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO
{
    // this is the structure of the incoming json object for program details
    public class IncomingProgramDetailsDTO
    {
        public string? id {  get; set; } = null;
        [ObjectAttributeValidator("Program Information")]
        public ProgramInfoDTO ProgramInfo { get; set; }
        [ObjectAttributeValidator("Additional Program Information")]

        public AddProgramInfoDTO AdditionalProgramInfo { get; set; }
    }
}
