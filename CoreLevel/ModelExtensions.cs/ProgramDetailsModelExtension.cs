
using CapitalPlacement.CoreLevel.DTO.ProgramDetailsDTO;
using CapitalPlacement.CoreLevel.Models;

namespace CapitalPlacement.CoreLevel.ModelExtensions.cs
{
    public static class ProgramDetailsModelExtension
    {
        public static NewApplicationFormModel ConvertIncomingDTOToModel(this IncomingProgramDetailsDTO dtoObject,
        bool newDoc=false, NewApplicationFormModel? applicationModel=null)
        {
            if (newDoc && applicationModel == null)
            {
                string finalId = Guid.NewGuid().ToString();
                return new NewApplicationFormModel
                {
                    id = finalId,
                    ProgramInfo = dtoObject.ProgramInfo,
                    AdditionalProgramInfo = dtoObject.AdditionalProgramInfo
                };
            }
            applicationModel.ProgramInfo = dtoObject.ProgramInfo;
            applicationModel.AdditionalProgramInfo = dtoObject.AdditionalProgramInfo;
            return applicationModel;

        }

        public static IncomingProgramDetailsDTO GetOutgoingProgramDetailsFromModel(this NewApplicationFormModel modelObj)
        {
            return new IncomingProgramDetailsDTO
            {
                id = modelObj.id,
                ProgramInfo = modelObj.ProgramInfo,
                AdditionalProgramInfo =modelObj.AdditionalProgramInfo
            };
        }
    }
}
