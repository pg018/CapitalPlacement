
using CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO;
using CapitalPlacement.Infrastructure;

namespace CapitalPlacement.CoreLevel.Models
{
    public class ProgramDetailsModel: CosmosDocument
    {
        public ProgramInfoDTO ProgramInfo {  get; set; }
        public AddProgramInfoDTO AdditionalProgramInfo {  get; set; }
    }
}
