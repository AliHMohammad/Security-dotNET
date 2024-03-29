using Security_CSharp.Security.DTOs;
using Security_CSharp.Security.Entitites;

namespace Security_CSharp.Security.Mappers
{
    public static class AuthMapper
    {

        public static UserResponse ToDTOUser(this User user)
        {
            return new UserResponse(
                    user.Username,
                    user.Email
                );
        }
    }
}
