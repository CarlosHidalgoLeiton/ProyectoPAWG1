using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        var exist =  user.IdUser != null && user.IdUser > 0;

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

}
    
    

