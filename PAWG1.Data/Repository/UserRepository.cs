using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

public interface IUserRepository
{
    Task<bool> DeleteUserAsync(User user);
    Task<User> GetUserAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> SaveUserAsync(User user);
}

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public async Task<User> SaveUserAsync(User user)
    {
        var exist = await ExistUser(user.IdUser);

        if (exist)
            await UpdateAsync(user);
        else
            await CreateAsync(user);

        var users = await ReadAsync();
        return users.SingleOrDefault(c => c.IdUser == user.IdUser)!;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await ReadAsync();

        return users;
    }

    public async Task<User> GetUserAsync(int id)
    {
        var users = await ReadAsync();

        return users.SingleOrDefault(c => c.IdUser == id)!;
    }

    public async Task<bool> DeleteUserAsync(User user)
    {
        return await DeleteAsync(user);
    }

    private async Task<bool> ExistUser(int? id)
    {
        if (id == null)
            return false;

        var users = await ReadAsync();

        var user = users.FirstOrDefault(c => c.IdUser == id);

        if (user == default)
            return true;

        return false;
    }
}
    
    

