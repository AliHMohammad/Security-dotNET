using Security_CSharp.Exceptions;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Mappers;
using System.Security.Cryptography;

namespace Security_CSharp.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        public async Task<UserResponse> register(SignupRequest request)
        {
            var userDbEmail = await _authRepository.GetUserByEmail(request.Email);
            var userDbUsername = await _authRepository.GetUserByUsername(request.Username);

            if (userDbEmail is not null) throw new BadRequestException($"User with email {request.Email} exists already");
            if (userDbUsername is not null) throw new BadRequestException($"User with username {request.Username} exists already");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var createdUser = await _authRepository.CreateUser(new User() { Email = request.Email, PasswordHash = passwordHash, Username = request.Username });

            return createdUser.ToDTOUser();
        }






        private void CreatePasswordHash(string plainPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainPassword));
            }
        }
    }
}
