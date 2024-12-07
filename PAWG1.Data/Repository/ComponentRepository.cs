using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for the component repository, defining methods for component operations.
/// </summary>
public interface IComponentRepository
{
    /// <summary>
    /// Deletes a component asynchronously.
    /// </summary>
    /// <param name="component">The component to delete.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    Task<bool> DeleteComponentAsync(Component component);

    /// <summary>
    /// Retrieves all components asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all components.</returns>
    Task<IEnumerable<Component>> GetAllComponentsAsync();

    /// <summary>
    /// Retrieves all components with active state asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all components with active state.</returns>
    Task<IEnumerable<Component>> GetAllComponentsDashboardAsync();

    /// <summary>
    /// Retrieves a component by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the component to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested component.</returns>
    Task<Component> GetComponentAsync(int id);

    /// <summary>
	/// Saves a collection of components asynchronously.
	/// </summary>
	/// <param name="categories">The collection of components to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    Task<Component> SaveComponentAsync(Component component);
}

/// <summary>
/// Implementation of the component repository, providing methods for component operations.
/// </summary>
public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
{
    /// <summary>
    /// Saves a component asynchronously, either creating a new one or updating an existing one.
    /// </summary>
    /// <param name="component">The component to save.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    public async Task<Component> SaveComponentAsync(Component component)
    {
        var exist = component.IdComponent != null && component.IdComponent > 0;

        if (exist)
            await UpdateAsync(component);
        else
            await CreateAsync(component);

        var components = await ReadAsync();
        return components.SingleOrDefault(c => c.IdComponent == component.IdComponent)!;
    }

    /// <summary>
	/// Retrieves all components asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation, containing all components.</returns>
    public async Task<IEnumerable<Component>> GetAllComponentsAsync()
    {
        var components = await ReadAsync();

        return components;
    }

    /// <summary>
	/// Retrieves all components with active state asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation, containing all components with active state.</returns>
    public async Task<IEnumerable<Component>> GetAllComponentsDashboardAsync()
    {
        var data = await ReadAsync();

        var components = data.Where(x => x.State == true);

        return components;
    }


    /// <summary>
	/// Retrieves a component by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the component to retrieve.</param>
	/// <returns>A task that represents the asynchronous operation, containing the requested component.</returns>
    public async Task<Component> GetComponentAsync(int id)
    {
        var components = await ReadAsync();

        return components.SingleOrDefault(c => c.IdComponent == id)!;
    }

    /// <summary>
	/// Deletes a component asynchronously.
	/// </summary>
	/// <param name="component">The component to delete.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteComponentAsync(Component component)
    {
        return await DeleteAsync(component);
    }

}
    
    

