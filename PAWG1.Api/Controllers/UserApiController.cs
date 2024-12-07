using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// API controller for managing user entities.
/// Provides endpoints for creating, updating, retrieving, and deleting users.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserApiController : Controller
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor for the controller. Injects the IUserService to be used in the controller's actions.
    /// </summary>
    /// <param name="userService">The service used to manage users.</param>
    public UserApiController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Action to save a new user.
    /// </summary>
    /// <param name="user">The user object to be saved.</param>
    /// <returns>The saved user object.</returns>
    [HttpPost("save", Name = "SaveUser")]
    public async Task<CMP.User> Save([FromBody] CMP.User user)
    {
        return await _userService.SaveUserAsync(user);
    }

    /// <summary>
    /// Action to update an existing user.
    /// </summary>
    /// <param name="user">The user object with updated values.</param>
    /// <returns>The updated user object.</returns>
    [HttpPut("{id}", Name = "UpdateUser")]
    public async Task<CMP.User> Update([FromBody] CMP.User user)
    {
        return await _userService.SaveUserAsync(user);
    }

    /// <summary>
    /// Action to retrieve a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user object corresponding to the provided ID.</returns>
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<CMP.User> Get(int id) {
        return await _userService.GetUserAsync(id);
    }

    /// <summary>
    /// Action to retrieve all users.
    /// </summary>
    /// <returns>A collection of all users.</returns>
    [HttpGet("all", Name = "GetAllUsers")]
    public async Task<IEnumerable<CMP.User>> GetAll() {
        return await _userService.GetAllUsersAsync();
    }

    /// <summary>
    /// Action to delete a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    [HttpDelete("{id}", Name = "DeleteUser")]
    public async Task<bool> Delete(int id) {
        return await _userService.DeleteUserAsync(id);
    }

}
