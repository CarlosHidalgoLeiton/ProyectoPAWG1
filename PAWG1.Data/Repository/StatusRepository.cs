using PAWG1.Models.EFModels;

namespace PAWG1.Data.Repository;

public interface IStatusRepository
{
    Task<bool> DeleteStatusAsync(Status status);
    Task<Status> SaveStatusAsync(Status status);
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}

public class StatusRepository : RepositoryBase<Status>, IStatusRepository
{
    public async Task<Status> SaveStatusAsync(Status status)
    {
        await CreateAsync(status);

        var statutes = await ReadAsync();
        return statutes.SingleOrDefault(c => (c.ComponentId == status.ComponentId) && (c.UserId == status.UserId))!;
    }

    public async Task<bool> DeleteStatusAsync(Status status)
    {
        return await DeleteAsync(status);
    }

    public async Task<IEnumerable<Status>> GetAllStatusesAsync() 
    {
        return await ReadAsync();    
    }
}
