using Microsoft.EntityFrameworkCore;
using Security_CSharp.Security.Entitites;
using System.Security.Cryptography;

namespace Security_CSharp.Seeds
{
    public static class SeedDataAuth
    {

        public static void SeedDataAuthInit(this ModelBuilder modelBuilder, IConfiguration configuration)
        {

            // SETTINGS:
            var adminUsername = "Admin";
            var adminEmail = "admin@kea.dk";
            // Admin password is saved in user secrets
            var adminPassword = configuration.GetSection("AppSettings:AdminPassword").Value ?? throw new Exception("AdminPassword is not set in user secrets.");


            // Create Roles
            var ADMIN = new Role() { Name = "ADMIN" };
            var USER = new Role() { Name = "USER" };
            modelBuilder.Entity<Role>().HasData(ADMIN, USER);


            // Remove Create Admin and je.HasData(..), if you dont wish to have an admin-user on startup

            // Create Admin
            using var hmac = new HMACSHA256();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(adminPassword));
            var adminUser = new User() { Username = adminUsername, Email = adminEmail, PasswordHash = passwordHash, PasswordSalt = passwordSalt };
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
                    // Assign ADMIN-role to Admin
                    je.HasData(
                        new { user_username = adminUsername, role_name = "ADMIN" }
                    );
                });
        }


    }
}
