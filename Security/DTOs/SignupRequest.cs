using System.ComponentModel.DataAnnotations;

namespace Security_CSharp.Security.DTOs
{
    public record SignupRequest
    (
        // TODO: Add signup-validation here

        [Required]
        [MinLength(4)]
        [MaxLength(14)]
        string Username,

        [Required]
        [EmailAddress]
        string Email,

        [MinLength(4)]
        [MaxLength(30)]
        [Required]
        string Password
    );
}
