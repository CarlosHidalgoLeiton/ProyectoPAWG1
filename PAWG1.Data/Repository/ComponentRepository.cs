using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWG1.Architecture.Helpers;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;


public interface IComponentRepository
{
    Task<bool> DeleteComponentAsync(Component component);
    Task<IEnumerable<Component>> GetAllComponentsAsync();
    Task<IEnumerable<Component>> GetAllDashboardAsync();
    Task<Component> GetComponentAsync(int id);
    Task<Component> SaveComponentAsync(Component component);

}

public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
{
    public async Task<Component> SaveComponentAsync(Component component)
    {
        var exist = component.IdComponent != null && component.IdComponent > 0;

        if (exist)
            await UpdateAsync(component);
        else
            await CreateAsync(component);

        var components = await ReadAsync();
        return components.SingleOrDefault(c => c.IdComponent == component.IdComponent)!;
    }

    public async Task<IEnumerable<Component>> GetAllComponentsAsync()
    {
        var components = await ReadAsync();

        return components;
    }

    public async Task<IEnumerable<Component>> GetAllDashboardAsync()
    {
        var data = await ReadAsync();

        var components = data.Where(x => x.State == true);

        return components;
    }

    public async Task<Component> GetComponentAsync(int id)
    {
        var components = await ReadAsync();

        return components.SingleOrDefault(c => c.IdComponent == id)!;
    }

    public async Task<bool> DeleteComponentAsync(Component component)
    {
        return await DeleteAsync(component);
    }

}
    
    

