using ApiContactbookApplication.Dtos;

namespace ApiContactbookApplication.Services.Contract
{
    public interface IStateService
    {
        ServiceResponse<IEnumerable<StateDto>> GetAllStates();

        //ServiceResponse<StateDto> GetStateById(int id);

        ServiceResponse<IEnumerable<StateDto>> GetStateByCountryId(int countyId);
    }
}
