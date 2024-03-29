using Microsoft.EntityFrameworkCore;
using Security_CSharp.Security.Entitites;

namespace Security_CSharp.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create Roles
            var ADMIN = new Role() { Name = "ADMIN" };
            var USER = new Role() { Name = "USER" };

            modelBuilder.Entity<Role>().HasData(ADMIN, USER);


        }

    }
}
