using Microsoft.EntityFrameworkCore;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Seeds;

namespace Security_CSharp.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        private readonly IConfiguration _configuration;


        public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedDataAuthInit(_configuration);
            //Add more Seed-data classes here
        }

    }
}
