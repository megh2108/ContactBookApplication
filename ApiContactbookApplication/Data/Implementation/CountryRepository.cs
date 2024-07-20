using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContactbookApplication.Data.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IAppDbContext _appDbContext;

        public CountryRepository(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            List<Country> countries = _appDbContext.Countries.ToList();
            return countries;
        }

        //public Country GetCountryById(int id)
        //{
        //    var country = _appDbContext.Countries.FirstOrDefault(c => c.CountryId == id);

        //    return country;
        //}
    }
}
