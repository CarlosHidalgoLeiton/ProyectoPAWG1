using PAWG1.Models;
using PAWG1.Data.Repository;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Models.EFModels;

namespace PAWG1.Service.Services;

public interface IComponentService
{
    Task<bool> DeleteComponentAsync(int id);
    Task<IEnumerable<Component>> GetAllComponentsAsync();
    Task<Component> GetComponentAsync(int id);
    Task<Component> SaveComponentAsync(Component component);
}

public class ComponentService : IComponentService
{
    private readonly IComponentRepository _componentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="CategoryRepository">The repository used to access Category data.</param>
    public ComponentService(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Category"/>.</returns>
    public async Task<CMP.Component> GetComponentAsync(int id)
    {
        return await _componentRepository.GetComponentAsync(id);
    }

    /// <summary>
    /// Asynchronously retrieves all Categorys.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<IEnumerable<CMP.Component>> GetAllComponentsAsync()
    {
        return await _componentRepository.GetAllComponentsAsync();
    }

    /// <summary>
    /// Asynchronously saves a new Category into the database.
    /// </summary>
	/// <param name="Category">The Category to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<CMP.Component> SaveComponentAsync(CMP.Component component)
    {
        return await _componentRepository.SaveComponentAsync(component);
    }

    /// <summary>
    /// Asynchronously deletes a Category from the database.
    /// </summary>
    /// <param name="Category">The Category to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<bool> DeleteComponentAsync(int id)
    {
        var components = await _componentRepository.GetAllComponentsAsync();
        var deletion = components.SingleOrDefault(x => x.IdComponent == id);
        return await _componentRepository.DeleteComponentAsync(deletion);
    }
}

