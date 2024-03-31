using Security_CSharp.Data;
using Security_CSharp.Exceptions;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Mappers;
using System.Security.Claims;

namespace Security_CSharp.Security.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> AddRole(string username, string role)
        {
            var userDb = await _userRepository.GetUserByUsername(username) ?? throw new NotFoundException($"Could not find user by given username: {username}");
            var roleDb = await _roleRepository.GetRoleByName(role) ?? throw new NotFoundException($"Could not find role by given name: {role}");

            userDb.Roles.Add(roleDb);
            await _unitOfWork.SaveChangesAsync();

            return userDb.ToDTOUser();
        }

        public async Task<UserResponse> RemoveRole(string username, string role)
        {
            var userDb = await _userRepository.GetUserByUsername(username) ?? throw new NotFoundException($"Could not find user by given username: {username}");
            var roleDb = await _roleRepository.GetRoleByName(role) ?? throw new NotFoundException($"Could not find role by given name: {role}");

            userDb.Roles.Remove(roleDb);
            await _unitOfWork.SaveChangesAsync();

            return userDb.ToDTOUser();
        }

        public async Task DeleteUser(ClaimsPrincipal principal)
        {
            var usernameClaim = principal.FindFirst("subject") ?? throw new BadRequestException("Username claim \"subject\" not found in principal.");

            var userdb = await _userRepository.GetUserByUsername(usernameClaim.Value)
                ?? throw new NotFoundException($"Could not find user by username in token: {usernameClaim.Value}");

            await _userRepository.DeleteUser(userdb);
        }
    }
}
