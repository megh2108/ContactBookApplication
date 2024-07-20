using ApiContactbookApplication.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContactbookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("GetAllStates")]
        public IActionResult GetAllStates()
        {
            var response = _stateService.GetAllStates();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        //[HttpGet("GetStateById")]
        //public IActionResult GetCountryById(int id)
        //{
        //    var response = _stateService.GetStateById(id);

        //    if (!response.Success)
        //    {
        //        return NotFound(response);
        //    }

        //    return Ok(response);
        //}

        [HttpGet("GetStateByCountryId/{countryId}")]
        public IActionResult GetStateByCountryId(int countryId)
        {
            var response = _stateService.GetStateByCountryId(countryId);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
