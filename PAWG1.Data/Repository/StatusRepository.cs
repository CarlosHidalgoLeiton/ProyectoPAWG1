using PAWG1.Models.EFModels;

namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for the status repository, defining methods for status operations.
/// </summary>
public interface IStatusRepository
{
    /// <summary>
    /// Deletes a status asynchronously.
    /// </summary>
    /// <param name="status">The status to delete.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    Task<bool> DeleteStatusAsync(Status status);

    /// <summary>
	/// Saves a status asynchronously, either creating a new one or updating an existing one.
	/// </summary>
	/// <param name="status">The status to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    Task<Status> SaveStatusAsync(Status status);

    /// <summary>
    /// Retrieves all status asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all status.</returns>
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}

/// <summary>
/// Implementation of the status repository, providing methods for status operations.
/// </summary>
public class StatusRepository : RepositoryBase<Status>, IStatusRepository
{
    /// <summary>
    /// Saves a status asynchronously, either creating a new one or updating an existing one.
    /// </summary>
    /// <param name="status">The status to save.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    public async Task<Status> SaveStatusAsync(Status status)
    {
        await CreateAsync(status);

        var statutes = await ReadAsync();
        return statutes.SingleOrDefault(c => (c.ComponentId == status.ComponentId) && (c.UserId == status.UserId))!;
    }

    /// <summary>
    /// Deletes a status asynchronously.
    /// </summary>
    /// <param name="status">The status to delete.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteStatusAsync(Status status)
    {
        return await DeleteAsync(status);
    }

    /// <summary>
    /// Retrieves all status asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all status.</returns>
    public async Task<IEnumerable<Status>> GetAllStatusesAsync() 
    {
        return await ReadAsync();    
    }
}
