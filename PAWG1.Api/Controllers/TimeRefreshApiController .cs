using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;
using PAWG1.Models.EFModels;

namespace PAW.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class TimeRefreshApiController : Controller
{
    private readonly ITimeRefreshService _timeRefreshService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeRefreshService"></param>
    public TimeRefreshApiController(ITimeRefreshService timeRefreshService)
    {
        _timeRefreshService = timeRefreshService;
    }

    [HttpPost("save", Name = "SaveTimeRefresh")]
    public async Task<CMP.TimeRefresh> Save([FromBody] CMP.TimeRefresh timeRefresh)
    {
        return await _timeRefreshService.SaveTimeRefreshAsync(timeRefresh);
    }

    [HttpPut("{id}", Name = "UpdateTimeRefresh")]
    public async Task<CMP.TimeRefresh> Update([FromBody] CMP.TimeRefresh timeRefresh)
    {
        return await _timeRefreshService.SaveTimeRefreshAsync(timeRefresh);
    }

    [HttpGet("{id}", Name = "GetTimeRefresh")]
    public async Task<CMP.TimeRefresh> Get(int id) {
        return await _timeRefreshService.GetTimeRefreshAsync(id);
    }
}
