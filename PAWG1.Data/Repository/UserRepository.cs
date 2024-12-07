using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

/// <summary>
/// Interface for the user repository, defining methods for user operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Deletes a user asynchronously.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    Task<bool> DeleteUserAsync(User user);

    /// <summary>
    /// Retrieves a user by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested user.</returns>
    Task<User> GetUserAsync(int id);

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all users.</returns>
    Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>
	/// Saves a user asynchronously, either creating a new one or updating an existing one.
	/// </summary>
	/// <param name="user">The user to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    Task<User> SaveUserAsync(User user);
}

public class UserRepository : RepositoryBase<User>, IUserRepository
{

    /// <summary>
	/// Saves a user asynchronously, either creating a new one or updating an existing one.
	/// </summary>
	/// <param name="user">The user to save.</param>
	/// <returns>A task that represents the asynchronous operation, with a value indicating whether the save operation was successful.</returns>
    public async Task<User> SaveUserAsync(User user)
    {
        var exist =  user.IdUser != null && user.IdUser > 0;

        if (exist)
            await UpdateAsync(user);
        else
            await CreateAsync(user);

        var users = await ReadAsync();
        return users.SingleOrDefault(c => c.IdUser == user.IdUser)!;
    }

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all users.</returns>
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await ReadAsync();

        return users;
    }

    /// <summary>
    /// Retrieves a user by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested user.</returns>
    public async Task<User> GetUserAsync(int id)
    {
        var users = await ReadAsync();

        return users.SingleOrDefault(c => c.IdUser == id)!;
    }

    /// <summary>
    /// Deletes a user asynchronously.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    /// <returns>A task that represents the asynchronous operation, with a value indicating whether the deletion was successful.</returns>
    public async Task<bool> DeleteUserAsync(User user)
    {
        return await DeleteAsync(user);
    }

}
    
    

