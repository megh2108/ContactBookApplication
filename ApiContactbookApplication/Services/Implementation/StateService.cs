using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Data.Implementation;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using ApiContactbookApplication.Services.Contract;

namespace ApiContactbookApplication.Services.Implementation
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public ServiceResponse<IEnumerable<StateDto>> GetAllStates()
        {
            var response = new ServiceResponse<IEnumerable<StateDto>>();
            var states = _stateRepository.GetAllStates();

            if (states != null && states.Any())
            {


                List<StateDto> stateDto = new List<StateDto>();
                foreach (var state in states)
                {
                    stateDto.Add(
                        new StateDto()
                        {
                            StateId = state.StateId,
                            StateName = state.StateName,
                            CountryId = state.CountryId,

                        });
                }
                response.Data = stateDto;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        //public ServiceResponse<StateDto> GetStateById(int id)
        //{
        //    var response = new ServiceResponse<StateDto>();
        //    var state = _stateRepository.GetStateById(id);

        //    if (state != null)
        //    {

        //        var stateDto = new StateDto()
        //        {
        //            StateId = state.StateId,
        //            StateName = state.StateName,
        //            CountryId = state.CountryId,

        //        };

        //        response.Data = stateDto;

        //    }
        //    else
        //    {
        //        response.Success = false;
        //        response.Message = "No record found !.";
        //    }

        //    return response;
        //}

        public ServiceResponse<IEnumerable<StateDto>> GetStateByCountryId(int countyId)
        {
            var response = new ServiceResponse<IEnumerable<StateDto>>();
            var states = _stateRepository.GetStateByCountryId(countyId);

            if (states != null && states.Any())
            {

                List<StateDto> stateDto = new List<StateDto>();
                foreach (var state in states)
                {
                    stateDto.Add(
                        new StateDto()
                        {
                            StateId = state.StateId,
                            StateName = state.StateName,
                            CountryId = state.CountryId,

                        });
                }
                response.Data = stateDto;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }
    }
}
