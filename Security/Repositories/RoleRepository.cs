using Microsoft.EntityFrameworkCore;
using Security_CSharp.Data;
using Security_CSharp.Security.Entitites;
using Security_CSharp.Security.Interfaces;

namespace Security_CSharp.Security.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _dataContext;

        public RoleRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await this._dataContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }


    }
}
