using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class RoleApiController : Controller
{
    private readonly IRoleService _roleService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public RoleApiController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [HttpGet("all", Name = "GetAllRoles")]
    public async Task<IEnumerable<CMP.Role>> GetAll() {
        return await _roleService.GetAllRolesAsync();
    }
}
