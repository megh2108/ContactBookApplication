using ApiContactbookApplication.Dtos;

namespace ApiContactbookApplication.Services.Contract
{
    public interface ICountryService
    {
        ServiceResponse<IEnumerable<CountryDto>> GetAllCountries();

        //ServiceResponse<CountryDto> GetContactsById(int id);
    }
}
