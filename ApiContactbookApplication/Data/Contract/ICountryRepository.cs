using ApiContactbookApplication.Models;

namespace ApiContactbookApplication.Data.Contract
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries();

        //Country GetCountryById(int id);
    }
}
