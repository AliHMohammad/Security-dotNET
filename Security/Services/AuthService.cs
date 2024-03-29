using Microsoft.IdentityModel.Tokens;
using Security_CSharp.Exceptions;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Security.Interfaces;
using Security_CSharp.Security.Mappers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Security_CSharp.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        private readonly int EXPIRATION_HOURS = 2;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            this._authRepository = authRepository;
            this._configuration = configuration;
        }

        public async Task<UserResponse> register(SignupRequest request)
        {
            var userDbEmail = await _authRepository.GetUserByEmail(request.Email);
            var userDbUsername = await _authRepository.GetUserByUsername(request.Username);

            if (userDbEmail is not null) throw new BadRequestException($"User with email {request.Email} exists already.");
            if (userDbUsername is not null) throw new BadRequestException($"User with username {request.Username} exists already.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var createdUser = await _authRepository.CreateUser(
                new User()
                {
                    Email = request.Email,
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                });

            return createdUser.ToDTOUser();
        }


        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var userDb = await _authRepository.GetUserByUsername(request.Username) ?? throw new BadRequestException("Wrong username or password");
            if (!VerifyPasswordHash(request.Password, userDb.PasswordHash, userDb.PasswordSalt)) throw new BadRequestException("Wrong username or password");


            return new LoginResponse() { Username = userDb.Username, Token = CreateToken(userDb) };
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AppSettings:SecretToken"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenPayload = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(EXPIRATION_HOURS),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenPayload);

            return jwt;
        }

        private void CreatePasswordHash(string plainPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainPassword));
            }
        }

        private bool VerifyPasswordHash(string inputtedPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputtedPassword));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
