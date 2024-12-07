using Microsoft.AspNetCore.Mvc;
using PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAWG1.Api.Controllers;

/// <summary>
/// API controller for managing status entities.
/// Provides endpoints for saving, deleting, and retrieving statuses.
/// </summary>
[ApiController]
[Route("[controller]")]
public class StatusApiController : Controller
{
    private readonly IStatusService _statusService;

    /// <summary>
    /// Constructor for the controller. Injects the IStatusService to be used in the controller's actions.
    /// </summary>
    /// <param name="statusService">The service used to manage statuses.</param>
    public StatusApiController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    /// <summary>
    /// Action to save a status.
    /// </summary>
    /// <param name="status">The status object to be saved, including ComponentId, UserId, and Type.</param>
    /// <returns>The saved status object.</returns>
    [HttpPost("save", Name = "SaveStatus")]
    public async Task<Status> Save([Bind("ComponentId", "UserId", "Type")][FromBody] Status status)
    {
        return await _statusService.SaveStatusAsync(status);
    }

    /// <summary>
    /// Action to delete a status.
    /// </summary>
    /// <param name="status">The status object to be deleted.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    [HttpPost("deleteStatus", Name = "DeleteStatus")]
    public async Task<bool> Delete([FromBody] Status status)
    {
        return await _statusService.DeleteStatusAsync(status);
    }

    /// <summary>
    /// Action to retrieve all statuses.
    /// </summary>
    /// <returns>A collection of all statuses.</returns>
    [HttpGet("all", Name = "GetStatuses")]
    public async Task<IEnumerable<Status>> GetAll()
    {
        return await _statusService.GetAllStatusesAsync();
    }

}
