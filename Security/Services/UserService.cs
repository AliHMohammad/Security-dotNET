using Security_CSharp.Exceptions;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Mappers;

namespace Security_CSharp.Security.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;


        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }

        public async Task<UserResponse> AddRole(string username, string role)
        {
            var userDb = await _userRepository.GetUserByUsername(username) ?? throw new NotFoundException($"Could not find user by given username: {username}");
            var roleDb = await _roleRepository.GetRoleByName(role) ?? throw new NotFoundException($"Could not find role by given name: {role}");

            userDb.Roles.Add(roleDb);
            await _userRepository.SaveChanges();

            return userDb.ToDTOUser();
        }

        public async Task<UserResponse> RemoveRole(string username, string role)
        {
            var userDb = await _userRepository.GetUserByUsername(username) ?? throw new NotFoundException($"Could not find user by given username: {username}");
            var roleDb = await _roleRepository.GetRoleByName(role) ?? throw new NotFoundException($"Could not find role by given name: {role}");

            userDb.Roles.Remove(roleDb);
            await _userRepository.SaveChanges();

            return userDb.ToDTOUser();
        }
    }
}
