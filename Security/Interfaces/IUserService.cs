using Security_CSharp.Security.DTOs;

namespace Security_CSharp.Security.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> AddRole(string username, string role);
        Task<UserResponse> RemoveRole(string username, string role);
    }
}
