using Microsoft.AspNetCore.Mvc;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Security.Interfaces;

namespace Security_CSharp.Security.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(SignupRequest request)
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
