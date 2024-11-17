using PAWG1.Models;
using PAWG1.Data.Repository;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Models.EFModels;

namespace PAWG1.Service.Services;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
}

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="CategoryRepository">The repository used to access Category data.</param>
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all Categorys.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<IEnumerable<CMP.Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllRolesAsync();
    }
}

