using ApiContactbookApplication.Dtos;
using ApiContactbookApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiContactbookApplication.Data
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Contactbook> Contactbooks { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public virtual IQueryable<ContactbookDtoSP> GetPaginatedContactsSP(string? search,int page, int pageSize, string sortOrder )
        {
            var searchParam = new SqlParameter("@Search", search?? (object)DBNull.Value);
            var pageParam = new SqlParameter("@Page", page);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);
            var sortOrderParam = new SqlParameter("@SortOrder", sortOrder);

            return Set<ContactbookDtoSP>().FromSqlRaw("dbo.GetAllContactsByPagination @Search, @Page , @PageSize,@SortOrder", searchParam,pageParam, pageSizeParam,sortOrderParam);
        }

        public virtual IQueryable<ContactbookDtoSP> GetAllContactsByBirthdayMonth(int month)
        {
            var monthParam = new SqlParameter("@Month", month);
           

            return Set<ContactbookDtoSP>().FromSqlRaw("dbo.GetAllContactsByBirthdayMonth @Month ", monthParam);
        }
        
        public virtual IQueryable<ContactbookDtoSP> GetAllContactsByStates(int state)
        {
            var stateParam = new SqlParameter("@State", state);
           

            return Set<ContactbookDtoSP>().FromSqlRaw("dbo.GetAllContactsByStates @State ", stateParam);
        }

        public virtual int GetAllContactsCountByCountry(int country)
        {
            var countryParam = new SqlParameter("@Country", country);

            return Set<CountDto>().FromSqlRaw("dbo.GetAllContactsCountByCountry @Country ", countryParam).AsEnumerable().FirstOrDefault().Count;
        }  
        
        public virtual int GetAllContactsCountByGender(string gender)
        {
            var genderParam = new SqlParameter("@Gender", gender);

            return Set<CountDto>().FromSqlRaw("dbo.GetAllContactsCountByGender @Gender ", genderParam).AsEnumerable().FirstOrDefault().Count;
        }


        public EntityState GetEntryState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        public void SetEntryState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class
        {
            Entry(entity).State = entityState;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasOne(p => p.Country)
                .WithMany(p => p.State)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State_Country");

            modelBuilder.Entity<ContactbookDtoSP>().HasNoKey().ToView(null);
            modelBuilder.Entity<CountDto>().HasNoKey().ToView(null);

            //modelBuilder.Entity<Contactbook>()
            //   .HasOne(p => p.Country)
            //   .WithMany(p => p.Contactbook)
            //   .HasForeignKey(p => p.CountryId)
            //   .OnDelete(DeleteBehavior.ClientSetNull)
            //   .HasConstraintName("FK_Contactbook_Country");

            //modelBuilder.Entity<Contactbook>()
            //  .HasOne(p => p.State)
            //  .WithMany(p => p.Contactbook)
            //  .HasForeignKey(p => p.StateId)
            //  .OnDelete(DeleteBehavior.ClientSetNull)
            //  .HasConstraintName("FK_Contactbook_State");

        }



    }
}
