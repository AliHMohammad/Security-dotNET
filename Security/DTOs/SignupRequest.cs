using System.ComponentModel.DataAnnotations;

namespace Security_CSharp.Security.DTOs
{
    public record SignupRequest
    (
        // TODO: Add signup-validation here

        [Required]
        string Username,
        [Required]
        string Email,
        [Required]
        string Password
    );
}
