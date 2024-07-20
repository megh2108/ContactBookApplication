using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiContactbookApplication.Data
{
    public interface IAppDbContext : IDbContext
    {
        public DbSet<Contactbook> Contactbooks { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }
        IQueryable<ContactbookDtoSP> GetPaginatedContactsSP(string? search,int page, int pageSize, string sortOrder );

        IQueryable<ContactbookDtoSP> GetAllContactsByBirthdayMonth(int month);

        IQueryable<ContactbookDtoSP> GetAllContactsByStates(int state);

        int GetAllContactsCountByCountry(int country);

        int GetAllContactsCountByGender(string gender);

    }
}
