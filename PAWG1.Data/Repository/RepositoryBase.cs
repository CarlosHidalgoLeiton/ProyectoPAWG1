using PAWG1.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for basic repository operations.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IRepositoryBase<T>
{
    /// <summary>
    /// Creates a mew emtity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    Task<bool> CreateAsync(T entity);

    /// <summary>
	/// Deletes an existing entity asynchronously.
	/// </summary>
	/// <param name="entity">The entity to be deleted.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
	/// Reads all entities of type T asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities.</returns>
    Task<IEnumerable<T>> ReadAsync();

    /// <summary>
	/// Updates an existing entity asynchronously.
	/// </summary>
	/// <param name="entity">The entity to be updated.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    Task<bool> UpdateAsync(T entity);
}

/// <summary>
/// Base class for repository operations.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly PawprojectContext _dataDbContext;

    protected PawprojectContext DbContext => _dataDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
    /// </summary>
    public RepositoryBase()
    {
        _dataDbContext = new PawprojectContext();
    }

    /// <summary>
	/// Creates an entity of type T asynchronously.
	/// </summary>
	/// <param name="entity">The entity to be saved in the database.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    public async Task<bool> CreateAsync(T entity)
    {
        try
        {
            await _dataDbContext.AddAsync(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in CreateAsync");
        }
    }

    /// <summary>
	/// Updates an existing entity of type T asynchronously.
	/// </summary>
	/// <param name="entity">The entity to be updated.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _dataDbContext.Update(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in UpdateAsync");
        }
    }

    /// <summary>
	/// Deletes an entity of type T asynchronously.
	/// </summary>
	/// <param name="entity">The entity to be deleted.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            _dataDbContext.Remove(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is an error in DeleteAsync");
        }
    }

    /// <summary>
	/// Reads all entities of type T asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities.</returns>
    public async Task<IEnumerable<T>> ReadAsync()
    {
        try
        {
            var query = _dataDbContext.Set<T>().AsQueryable();

            if (typeof(T) == typeof(Component))
            {
                query = query.Include("Statuses");
            }
            else if (typeof(T) == typeof(User)) {
                query = query.Include("IdRoleNavigation");
            }

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is an error in ReadAsync", ex);
        }
    }

    /// <summary>
	/// Saves changes to the database asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    protected async Task<bool> SaveAsync()
    {
        var result = await _dataDbContext.SaveChangesAsync();
        return result > 0;
    }


}
