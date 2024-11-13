using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserApiController : Controller
{
    private readonly IUserService _userService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public UserApiController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("save", Name = "SaveUser")]
    public async Task<CMP.User> Save([FromBody] CMP.User User)
    {
        return await _userService.SaveUserAsync(User);
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<CMP.User> Update([FromBody] CMP.User User)
    {
        return await _userService.SaveUserAsync(User);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<CMP.User> Get(int id) {
        return await _userService.GetUserAsync(id);
    }

    [HttpGet("all", Name = "GetAllUsers")]
    public async Task<IEnumerable<CMP.User>> GetAll() {
        return await _userService.GetAllUsersAsync();
    }

    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<bool> Delete(int id) {
        return await _userService.DeleteUserAsync(id);
    }




}
