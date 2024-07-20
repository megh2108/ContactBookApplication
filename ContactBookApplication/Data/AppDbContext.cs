using Microsoft.EntityFrameworkCore;
using ContactBookApplication.Models;

namespace ContactBookApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Contactbook> Contacts { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
