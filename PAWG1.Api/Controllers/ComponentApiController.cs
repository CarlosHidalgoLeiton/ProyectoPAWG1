using Microsoft.AspNetCore.Mvc;
using PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class ComponentApiController : Controller
{
    private readonly IComponentService _componentService;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="componentService"></param>
    public ComponentApiController(IComponentService productService)
    {
        _componentService = productService;

	}


    [HttpPost("save", Name = "SaveProduct")]
    public async Task<Component> Save([FromBody] Component Component)
    {
        return await _componentService.SaveComponentAsync(Component);
    }

}
