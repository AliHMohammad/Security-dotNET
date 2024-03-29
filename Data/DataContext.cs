using Microsoft.EntityFrameworkCore;
using Security_CSharp.Security.Entitites;

namespace Security_CSharp.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User> Users { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {

        }


    }
}
