
using CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO;
using CapitalPlacement.CoreLevel.Models;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class ProgramDetailsModelExtension
    {
        public static ProgramDetailsModel ConvertIncomingDTOToModel(this IncomingProgramDetailsDTO dtoObject, bool newDoc=false)
        {
            string finalId = dtoObject.id;
            if (newDoc)
            {
                finalId = Guid.NewGuid().ToString();
            }
            return new ProgramDetailsModel
            {
                id = finalId,
                ProgramInfo = dtoObject.ProgramInfo,
                AdditionalProgramInfo = dtoObject.AdditionalProgramInfo
            };
        }
    }
}
