namespace Security_CSharp.Security.DTOs
{
    public record UserResponse(
            string Username,
            string Email,
            List<string> Roles
        );

}
