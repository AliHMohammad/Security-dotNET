using Security_CSharp.Security.DTOs;

namespace Security_CSharp.Security.Interfaces
{
    public interface IAuthService
    {

        Task<UserResponse> register(SignupRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<UserResponse> AddRole(string username, string role);
        Task<UserResponse> RemoveRole(string username, string role);

    }
}
