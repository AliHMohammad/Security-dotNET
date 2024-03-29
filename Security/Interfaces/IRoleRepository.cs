using Security_CSharp.Security.Entitites;

namespace Security_CSharp.Security.Interfaces
{
    public interface IRoleRepository
    {

        Task<Role?> GetRoleByName(string name);
    }
}
