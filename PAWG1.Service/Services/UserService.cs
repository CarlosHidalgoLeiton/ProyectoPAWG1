using PAWG1.Data.Repository;
using PAWG1.Models.EFModels;
using CMP = PAWG1.Models.EFModels;
namespace PAWG1.Service.Services;

/// <summary>
/// Interface for user-related services.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Asynchronously deletes a user from the database.
    /// </summary>
    /// <param name="user">The user to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    Task<bool> DeleteUserAsync(int id);

    /// <summary>
    /// Asynchronously retrieves all users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>
    /// Asynchronously retrieves a user  by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="User/>.</returns>
    Task<User> GetUserAsync(int id);

    /// <summary>
    /// Asynchronously saves a new user into the database.
    /// </summary>
    /// <param name="user">The user to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    Task<User> SaveUserAsync(User user);
}
/// <summary>
/// Implementation of the <see cref="IUserService"/> interface.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="UserRepository">The repository used to access User data.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a User by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the User to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="User"/>.</returns>
    public async Task<CMP.User> GetUserAsync(int id)
    {
        return await _userRepository.GetUserAsync(id);
    }

    /// <summary>
    /// Asynchronously retrieves all Users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    public async Task<IEnumerable<CMP.User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    /// <summary>
    /// Asynchronously saves a new User into the database.
    /// </summary>
	/// <param name="Category">The User to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    public async Task<CMP.User> SaveUserAsync(CMP.User user)
    {
        return await _userRepository.SaveUserAsync(user);
    }

    /// <summary>
    /// Asynchronously deletes a User from the database.
    /// </summary>
    /// <param name="User">The User to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="User"/>.</returns>
    public async Task<bool> DeleteUserAsync(int id)
    {
        var users = await _userRepository.GetAllUsersAsync();
        var deletion = users.SingleOrDefault(x => x.IdUser == id);
        return await _userRepository.DeleteUserAsync(deletion);
    }
}

