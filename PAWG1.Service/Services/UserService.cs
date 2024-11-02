using PAWG1.Models;
using PAWG1.Data.Repository;
using CMP = PAWG1.Models.EFModels;
using PAWG1.Models.EFModels;

namespace PAWG1.Service.Services;

public interface IUserService
{
    Task<bool> DeleteUserAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int id);
    Task<User> SaveUserAsync(User user);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="CategoryRepository">The repository used to access Category data.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Asynchronously retrieves a Category by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Category to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the requested <see cref="Category"/>.</returns>
    public async Task<CMP.User> GetUserAsync(int id)
    {
        return await _userRepository.GetUserAsync(id);
    }

    /// <summary>
    /// Asynchronously retrieves all Categorys.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<IEnumerable<CMP.User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    /// <summary>
    /// Asynchronously saves a new Category into the database.
    /// </summary>
	/// <param name="Category">The Category to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<CMP.User> SaveUserAsync(CMP.User user)
    {
        return await _userRepository.SaveUserAsync(user);
    }

    /// <summary>
    /// Asynchronously deletes a Category from the database.
    /// </summary>
    /// <param name="Category">The Category to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Category"/>.</returns>
    public async Task<bool> DeleteUserAsync(int id)
    {
        var users = await _userRepository.GetAllUsersAsync();
        var deletion = users.SingleOrDefault(x => x.IdUser == id);
        return await _userRepository.DeleteUserAsync(deletion);
    }
}

