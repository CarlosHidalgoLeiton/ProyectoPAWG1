using PAWG1.Data.Repository;
using PAWG1.Models.EFModels;
using CMP = PAWG1.Models.EFModels;
namespace PAWG1.Service.Services;

/// <summary>
/// Interface for role-related services.
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Asynchronously retrieves all roles.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Role"/>.</returns>
    Task<IEnumerable<Role>> GetAllRolesAsync();

    /// <summary>
    /// Asynchronously retrieves a role by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Role"/>.</returns>
    Task<CMP.Role> GetRoleAsync(int id);
}


/// <summary>
/// Implementation of the <see cref="IRoleService"/> interface.
/// </summary>
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleService"/> class.
    /// </summary>
    /// <param name="roleRepository">The repository used to access Role data.</param>
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all Roles.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Role"/>.</returns>
    public async Task<IEnumerable<CMP.Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllRolesAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a role by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the role to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Role"/>.</returns>
    public async Task<CMP.Role> GetRoleAsync(int id)
    {
        return await _roleRepository.GetRoleAsync(id);
    }
}

