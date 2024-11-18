using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role> GetRoleAsync(int id);
}

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{


    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var roles = await ReadAsync();

        return roles;
    }

    public async Task<Role> GetRoleAsync(int id)
    {
        var roles = await ReadAsync();

        return roles.SingleOrDefault(c => c.IdRole == id)!;
    }
}
    
    

