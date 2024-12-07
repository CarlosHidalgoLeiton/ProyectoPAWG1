using PAWG1.Data.Repository;
using PAWG1.Models.EFModels;
namespace PAWG1.Service.Services;

/// <summary>
/// Interface for Status-related services.
/// </summary>
public interface IStatusService
{
    /// <summary>
    /// Asynchronously deletes a Status from the database.
    /// </summary>
    /// <param name="category">The Status to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    Task<bool> DeleteStatusAsync(Status status);
    /// <summary>
    /// Asynchronously saves a new Status into the database.
    /// </summary>
    /// <param name="status">The Status to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    Task<Status> SaveStatusAsync(Status status);

    /// <summary>
    /// Asynchronously retrieves all Status.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}


/// <summary>
/// Implementation of the <see cref="IStatusService"/> interface.
/// </summary>
public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusService"/> class.
    /// </summary>
    /// <param name="statusRepository">The repository used to access status data.</param>
    /// 

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    /// <summary>
    /// Asynchronously saves a new status into the database.
    /// </summary>
    /// <param name="status">The status to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    public async Task<Status> SaveStatusAsync(Status status)
    {
        return await _statusRepository.SaveStatusAsync(status);
    }

    /// <summary>
    /// Asynchronously deletes a status from the database.
    /// </summary>
    /// <param name="status">The status to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    public async Task<bool> DeleteStatusAsync(Status status)
    {
        return await _statusRepository.DeleteStatusAsync(status);
    }

    /// <summary>
    /// Asynchronously retrieves all statuses.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Status"/>.</returns>
    public async Task<IEnumerable<Status>> GetAllStatusesAsync() 
    {
        return await _statusRepository.GetAllStatusesAsync();
    }
}
