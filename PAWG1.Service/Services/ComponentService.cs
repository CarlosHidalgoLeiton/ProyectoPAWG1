using PAWG1.Data.Repository;
using PAWG1.Models.EFModels;
using CMP = PAWG1.Models.EFModels;
namespace PAWG1.Service.Services;


/// <summary>
/// Interface for Component-related services.
/// </summary>
public interface IComponentService
{
    /// <summary>
    /// Asynchronously deletes a component from the database.
    /// </summary>
    /// <param name="component">The component to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    Task<bool> DeleteComponentAsync(int id);

    /// <summary>
    /// Asynchronously retrieves all components.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    Task<IEnumerable<Component>> GetAllComponentsAsync();

    /// <summary>
    /// Asynchronously retrieves all dashboards.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    Task<IEnumerable<CMP.Component>> GetAllDashboardAsync();

    /// <summary>
    /// Asynchronously retrieves a component by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the component to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Component"/>.</returns>
    Task<Component> GetComponentAsync(int id);

    /// <summary>
    /// Asynchronously saves a new component into the database.
    /// </summary>
    /// <param name="Component">The component to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    Task<Component> SaveComponentAsync(Component component);
}


/// <summary>
/// Implementation of the <see cref="IComponentService"/> interface.
/// </summary>
public class ComponentService : IComponentService
{
    private readonly IComponentRepository _componentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentService"/> class.
    /// </summary>
    /// <param name="ComponentRepository">The repository used to access Component data.</param>
    public ComponentService(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a Component by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Component to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Component"/>.</returns>
    public async Task<CMP.Component> GetComponentAsync(int id)
    {
        return await _componentRepository.GetComponentAsync(id);
    }

    /// <summary>
    /// Asynchronously retrieves all Components.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    public async Task<IEnumerable<CMP.Component>> GetAllComponentsAsync()
    {
        
        return await _componentRepository.GetAllComponentsAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all dashboards.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    public async Task<IEnumerable<CMP.Component>> GetAllDashboardAsync() 
    {
        return await _componentRepository.GetAllComponentsDashboardAsync();
    }

    /// <summary>
    /// Asynchronously saves a new Component into the database.
    /// </summary>
	/// <param name="Component">The Component to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    public async Task<CMP.Component> SaveComponentAsync(CMP.Component component)
    {
        return await _componentRepository.SaveComponentAsync(component);
    }

    /// <summary>
    /// Asynchronously deletes a Component from the database.
    /// </summary>
    /// <param name="Component">The Component to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Component"/>.</returns>
    public async Task<bool> DeleteComponentAsync(int id)
    {
        var components = await _componentRepository.GetAllComponentsAsync();
        var deletion = components.SingleOrDefault(x => x.IdComponent == id);
        return await _componentRepository.DeleteComponentAsync(deletion);
    }
}

