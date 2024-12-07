using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for the timerefresh repository, defining methods for timerefresh operations.
/// </summary>
public interface ITimeRefreshRepository
{

    /// <summary>
    /// Retrieves a timerefresh by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the timerefresh to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested timerefresh.</returns>
    Task<TimeRefresh> GetTimeRefreshAsync(int id);

    /// <summary>
	/// Saves a timerefresh asynchronously, either creating a new one or updating an existing one.
	/// </summary>
	/// <param name="timeRefresh">The timerefresh to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    Task<TimeRefresh> SaveTimeRefreshAsync(TimeRefresh timeRefresh);

}

/// <summary>
/// Implementation of the timerefresh repository, providing methods for timerefresh operations.
/// </summary>
public class TimeRefreshRepository : RepositoryBase<TimeRefresh>, ITimeRefreshRepository
{
    /// <summary>
	/// Saves a timerefresh asynchronously, either creating a new one or updating an existing one.
	/// </summary>
	/// <param name="timeRefresh">The timerefresh to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    public async Task<TimeRefresh> SaveTimeRefreshAsync(TimeRefresh timeRefresh)
    {
        var existingTimeRefresh = timeRefresh.TimeRefreshId != null && timeRefresh.TimeRefreshId > 0;

        if (existingTimeRefresh)
        {
            await UpdateAsync(timeRefresh);
        }
        else
            await CreateAsync(timeRefresh);

        var timeRefreshs = await ReadAsync();
        return timeRefreshs.SingleOrDefault(t => t.TimeRefreshId == timeRefresh.TimeRefreshId)!;
    }

    /// <summary>
    /// Retrieves a timerefresh by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the timerefresh to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested timerefresh.</returns>
    public async Task<TimeRefresh> GetTimeRefreshAsync(int id)
    {
        var timeRefreshs = await ReadAsync();
        return timeRefreshs.SingleOrDefault(t => t.TimeRefreshId == id);
    }

}
    
    

