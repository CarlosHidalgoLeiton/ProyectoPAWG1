using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;


public interface ITimeRefreshRepository
{
    Task<TimeRefresh> GetTimeRefreshAsync(int id);
    Task<TimeRefresh> SaveTimeRefreshAsync(TimeRefresh timeRefresh);

}

public class TimeRefreshRepository : RepositoryBase<TimeRefresh>, ITimeRefreshRepository
{
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

    public async Task<TimeRefresh> GetTimeRefreshAsync(int id)
    {
        var timeRefreshs = await ReadAsync();
        return timeRefreshs.SingleOrDefault(t => t.TimeRefreshId == id);
    }

}
    
    

