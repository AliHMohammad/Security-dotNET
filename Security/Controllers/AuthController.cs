using Microsoft.AspNetCore.Mvc;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Interfaces;

namespace Security_CSharp.Security.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;


        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(SignupRequest request)
        {
            return Ok(await _authService.register(request));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            return Ok(await _authService.Login(request));
        }



    }
}
