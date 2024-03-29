using Security_CSharp.Security.Entitites;

namespace Security_CSharp.Security.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
    }
}
