using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for the role repository, defining methods for role operations.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Retrieves all roles asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all roles.</returns>
    Task<IEnumerable<Role>> GetAllRolesAsync();

    /// <summary>
    /// Retrieves a role by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested role.</returns>
    Task<Role> GetRoleAsync(int id);
}

/// <summary>
/// Implementation of the role repository, providing methods for role operations.
/// </summary>
public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    /// <summary>
    /// Retrieves all roles asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all roles.</returns>
    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var roles = await ReadAsync();

        return roles;
    }

    /// <summary>
    /// Retrieves a role by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested role.</returns>
    public async Task<Role> GetRoleAsync(int id)
    {
        var roles = await ReadAsync();

        return roles.SingleOrDefault(c => c.IdRole == id)!;
    }
}
    
    

