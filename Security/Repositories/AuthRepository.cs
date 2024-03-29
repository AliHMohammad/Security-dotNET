using Microsoft.EntityFrameworkCore;
using Security_CSharp.Data;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Security.Interfaces;

namespace Security_CSharp.Security.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _dataContext.Users.FindAsync(username);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUser(User user)
        {
            var createdUser = await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return createdUser.Entity;
        }

    }
}
