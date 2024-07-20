using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Services.Contract;

namespace ApiContactbookApplication.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public ServiceResponse<IEnumerable<CountryDto>> GetAllCountries()
        {
            var response = new ServiceResponse<IEnumerable<CountryDto>>();
            var contries = _countryRepository.GetAllCountries();

            if (contries != null && contries.Any())
            {
               

                List<CountryDto> countryDto = new List<CountryDto>();
                foreach (var country in contries)
                {
                    countryDto.Add(
                        new CountryDto()
                        {
                            CountryId = country.CountryId,
                            CountryName = country.CountryName,
                           
                        });
                }
                response.Data = countryDto;

            }
            else
            {
                response.Success = false;
                response.Message = "No record found !.";
            }

            return response;
        }

        //public ServiceResponse<CountryDto> GetContactsById(int id)
        //{
        //    var response = new ServiceResponse<CountryDto>();
        //    var country = _countryRepository.GetCountryById(id);

        //    if (country != null)
        //    {

        //        var countryDto = new CountryDto()
        //        {
        //            CountryId = country.CountryId,
        //            CountryName = country.CountryName,
        //        };

        //        response.Data = countryDto;

        //    }
        //    else
        //    {
        //        response.Success = false;
        //        response.Message = "No record found !.";
        //    }

        //    return response;
        //}
    }
}
