using PAWG1.Models;
using PAWG1.Data.Repository;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Models.EFModels;

namespace PAWG1.Service.Services;

public interface ITimeRefreshService
{
    Task<TimeRefresh> GetTimeRefreshAsync(int id);
    Task<TimeRefresh> SaveTimeRefreshAsync(TimeRefresh timeRefresh);
}

public class TimeRefreshService : ITimeRefreshService
{
    private readonly ITimeRefreshRepository _timeRefreshRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="CategoryRepository">The repository used to access Category data.</param>
    public TimeRefreshService(ITimeRefreshRepository timeRefreshRepository)
    {
        _timeRefreshRepository = timeRefreshRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Category"/>.</returns>
    public async Task<CMP.TimeRefresh> GetTimeRefreshAsync(int id)
    {
        return await _timeRefreshRepository.GetTimeRefreshAsync(id);
    }

    /// <summary>
    /// Asynchronously saves a new Category into the database.
    /// </summary>
	/// <param name="Category">The Category to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<CMP.TimeRefresh> SaveTimeRefreshAsync(CMP.TimeRefresh timeRefresh)
    {
        return await _timeRefreshRepository.SaveTimeRefreshAsync(timeRefresh);
    }
}

