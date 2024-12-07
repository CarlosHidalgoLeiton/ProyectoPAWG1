using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// API controller for managing "TimeRefresh" operations.
/// Allows creating, updating, and retrieving TimeRefresh records.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TimeRefreshApiController : Controller
{
    private readonly ITimeRefreshService _timeRefreshService;

    /// <summary>
    /// Controller constructor, injects the ITimeRefreshService.
    /// </summary>
    /// <param name="timeRefreshService">Service that handles the business logic for TimeRefresh.</param>
    public TimeRefreshApiController(ITimeRefreshService timeRefreshService)
    {
        _timeRefreshService = timeRefreshService;
    }

    /// <summary>
    /// Action to save a new TimeRefresh object in the database.
    /// </summary>
    /// <param name="timeRefresh">TimeRefresh object to be saved.</param>
    /// <returns>The saved TimeRefresh object.</returns>
    [HttpPost("save", Name = "SaveTimeRefresh")]
    public async Task<CMP.TimeRefresh> Save([FromBody] CMP.TimeRefresh timeRefresh)
    {
        return await _timeRefreshService.SaveTimeRefreshAsync(timeRefresh);
    }

    /// <summary>
    /// Action to update an existing TimeRefresh object.
    /// </summary>
    /// <param name="timeRefresh">TimeRefresh object with updated values.</param>
    /// <returns>The updated TimeRefresh object.</returns>
    [HttpPut("{id}", Name = "UpdateTimeRefresh")]
    public async Task<CMP.TimeRefresh> Update([FromBody] CMP.TimeRefresh timeRefresh)
    {
        return await _timeRefreshService.SaveTimeRefreshAsync(timeRefresh);
    }

    /// <summary>
    /// Action to get a TimeRefresh object by its ID.
    /// </summary>
    /// <param name="id">ID of the TimeRefresh to retrieve.</param>
    /// <returns>The TimeRefresh object corresponding to the provided ID.</returns>
    [HttpGet("{id}", Name = "GetTimeRefresh")]
    public async Task<CMP.TimeRefresh> Get(int id) {
        return await _timeRefreshService.GetTimeRefreshAsync(id);
    }
}
