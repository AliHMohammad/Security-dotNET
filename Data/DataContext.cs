using Microsoft.EntityFrameworkCore;
using Security_CSharp.Security.Entitites;
using System.Security.Cryptography;

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

            using var hmac = new HMACSHA256();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("adminkode"));
            var adminUser = new User() { Username = "Admin", Email = "Admin@kea.dk", PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            //adminUser.Roles.Add(ADMIN);
            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                "RoleUser",
                r => r.HasOne<Role>().WithMany().HasForeignKey("role_name"),
                l => l.HasOne<User>().WithMany().HasForeignKey("user_username"),
                je =>
                {
                    je.HasKey("user_username", "role_name");
                    je.HasData(
                        new { user_username = "Admin", role_name = "ADMIN" }
                    );
                });




        }

    }
}
