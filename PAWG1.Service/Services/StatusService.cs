using PAWG1.Data.Repository;
using PAWG1.Models.EFModels;

namespace PAWG1.Service.Services;

public interface IStatusService
{
    Task<bool> DeleteStatusAsync(Status status);
    Task<Status> SaveStatusAsync(Status status);
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<Status> SaveStatusAsync(Status status)
    {
        return await _statusRepository.SaveStatusAsync(status);
    }

    public async Task<bool> DeleteStatusAsync(Status status)
    {
        return await _statusRepository.DeleteStatusAsync(status);
    }

    public async Task<IEnumerable<Status>> GetAllStatusesAsync() 
    {
        return await _statusRepository.GetAllStatusesAsync();
    }
}
