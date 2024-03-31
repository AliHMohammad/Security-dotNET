using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Interfaces;

namespace Security_CSharp.Security.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPatch("{username}/roles/add/{role}")]
        public async Task<ActionResult<UserResponse>> AddRole(string username, string role)
        {
            return Ok(await _userService.AddRole(username, role));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPatch("{username}/roles/remove/{role}")]
        public async Task<ActionResult<UserResponse>> RemoveRole(string username, string role)
        {
            return Ok(await _userService.RemoveRole(username, role));
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {
            // Get principal by the assigned token
            var principal = HttpContext.User;

            await _userService.DeleteUser(principal);
            return NoContent();
        }

        //TODO:
        // Missing implementations

        // 1. Get Users
        // 2. Get single User
        // 3. Update (put) User
    }
}
