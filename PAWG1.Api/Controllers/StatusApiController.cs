using Microsoft.AspNetCore.Mvc;
using PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAWG1.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusApiController : Controller
{
    private readonly IStatusService _statusService;

    public StatusApiController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpPost("save", Name = "SaveStatus")]
    public async Task<Status> Save([Bind("ComponentId", "UserId", "Type")][FromBody] Status status)
    {
        return await _statusService.SaveStatusAsync(status);
    }

    [HttpPost("deleteStatus", Name = "DeleteStatus")]
    public async Task<bool> Delete([FromBody] Status status)
    {
        return await _statusService.DeleteStatusAsync(status);
    }

    [HttpGet("all", Name = "GetStatuses")]
    public async Task<IEnumerable<Status>> GetAll()
    {
        return await _statusService.GetAllStatusesAsync();
    }

}
