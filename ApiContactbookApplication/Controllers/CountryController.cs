using ApiContactbookApplication.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiContactbookApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("GetAllCountries")]
        public IActionResult GetAllCountries()
        {
            var response = _countryService.GetAllCountries();

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        //[HttpGet("GetCountryById")]
        //public IActionResult GetCountryById(int id)
        //{
        //    var response = _countryService.GetContactsById(id);

        //    if (!response.Success)
        //    {
        //        return NotFound(response);
        //    }

        //    return Ok(response);
        //}
    }
}
