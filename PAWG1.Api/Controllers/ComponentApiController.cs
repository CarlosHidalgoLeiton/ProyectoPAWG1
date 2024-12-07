using Microsoft.AspNetCore.Mvc;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Service.Services;

namespace PAW.Api.Controllers;

/// <summary>
/// API controller for managing components.
/// Provides endpoints for creating, updating, retrieving, and deleting components.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ComponentApiController : Controller
{
    private readonly IComponentService _componentService;

    /// <summary>
    /// Constructor for the controller. Injects the IComponentService to be used in the actions.
    /// </summary>
    /// <param name="componentService">The service used to manage components.</param>
    public ComponentApiController(IComponentService componentService)
    {
        _componentService = componentService;
    }

    /// <summary>
    /// Action to save a new component.
    /// </summary>
    /// <param name="component">The component object to be saved.</param>
    /// <returns>The saved component object.</returns
    [HttpPost("save", Name = "SaveComponent")]
    public async Task<CMP.Component> Save([FromBody] CMP.Component component)
    {
        return await _componentService.SaveComponentAsync(component);
    }

    /// <summary>
    /// Action to update an existing component.
    /// </summary>
    /// <param name="component">The component object with updated values.</param>
    /// <returns>The updated component object.</returns>
    [HttpPut("{id}", Name = "UpdateComponent")]
    public async Task<CMP.Component> Update([FromBody] CMP.Component component)
    {
        return await _componentService.SaveComponentAsync(component);
    }

    /// <summary>
    /// Action to retrieve a component by its ID.
    /// </summary>
    /// <param name="id">The ID of the component to retrieve.</param>
    /// <returns>The component object corresponding to the provided ID.</returns>
    [HttpGet("{id}", Name = "GetComponent")]
    public async Task<CMP.Component> Get(int id) {
        return await _componentService.GetComponentAsync(id);
    }

    /// <summary>
    /// Action to retrieve all components.
    /// </summary>
    /// <returns>A collection of all components.</returns>
    [HttpGet("all", Name = "GetAllComponents")]
    public async Task<IEnumerable<CMP.Component>> GetAll() {
        return await _componentService.GetAllComponentsAsync();
    }

    /// <summary>
    /// Action to retrieve components specifically for the dashboard view.
    /// </summary>
    /// <returns>A collection of components suitable for display in a dashboard.</returns>
    [HttpGet("dashboard", Name = "GetAllDashboard")]
    public async Task<IEnumerable<CMP.Component>> GetAllDashboard()
    {
        var components = await _componentService.GetAllDashboardAsync();
        return components;
    }

    /// <summary>
    /// Action to delete a component by its ID.
    /// </summary>
    /// <param name="id">The ID of the component to delete.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    [HttpDelete("{id}", Name = "DeleteComponent")]
    public async Task<bool> Delete(int id) {
        return await _componentService.DeleteComponentAsync(id);
    }




}
