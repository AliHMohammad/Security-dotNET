namespace Security_CSharp.Security.DTOs
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

}
