using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
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

    [HttpPost("save", Name = "SaveComponent")]
    public async Task<CMP.Component> Save([FromBody] CMP.Component component)
    {
        return await _componentService.SaveComponentAsync(component);
    }

    [HttpPost("favorite", Name = "FavoriteComponent")]
    public async Task<CMP.Component> Favorite([FromBody] CMP.Component component)
    {
        CMP.User user = new CMP.User()
        {
            IdUser = 1,
            Email = "carloshl349@gmail.com",
            State = true,
            Username = "Carlos",
            Password = "hola1234",
            IdRole = 1
        };

        component.Users.Add(user);

        var result = await _componentService.SaveComponentAsync(component);

        return result;
    }

    [HttpPut("{id}", Name = "UpdateComponent")]
    public async Task<CMP.Component> Update([FromBody] CMP.Component component)
    {
        return await _componentService.SaveComponentAsync(component);
    }

    [HttpGet("{id}", Name = "GetComponent")]
    public async Task<CMP.Component> Get(int id) {
        return await _componentService.GetComponentAsync(id);
    }

    [HttpGet("all", Name = "GetAllComponents")]
    public async Task<IEnumerable<CMP.Component>> GetAll() {
        var components = await _componentService.GetAllComponentsAsync();
        return components;
    }

    [HttpDelete("{id}", Name = "DeleteComponent")]
    public async Task<bool> Delete(int id) {
        return await _componentService.DeleteComponentAsync(id);
    }




}
