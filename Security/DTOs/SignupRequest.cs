namespace Security_CSharp.Security.DTOs
{
    public record SignupRequest
    (
        string Username,
        string Email,
        string Password
    );
}
