using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// API controller for managing roles.
/// Provides endpoints to retrieve roles and all available roles.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RoleApiController : Controller
{
    private readonly IRoleService _roleService;

    /// <summary>
    /// Constructor for the controller. Injects the IRoleService to be used in the controller's actions.
    /// </summary>
    /// <param name="roleService">The service used to manage roles.</param>
    public RoleApiController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Action to retrieve all roles.
    /// </summary>
    /// <returns>A collection of all roles.</returns>
    [HttpGet("all", Name = "GetAllRoles")]
    public async Task<IEnumerable<CMP.Role>> GetAll() {
        return await _roleService.GetAllRolesAsync();
    }

    /// <summary>
    /// Action to retrieve a role by its ID.
    /// </summary>
    /// <param name="id">The ID of the role to retrieve.</param>
    /// <returns>The role object corresponding to the provided ID.</returns>
    [HttpGet("{id}", Name = "GetRole")]
    public async Task<CMP.Role> Get(int id)
    {
        return await _roleService.GetRoleAsync(id);
    }
}
