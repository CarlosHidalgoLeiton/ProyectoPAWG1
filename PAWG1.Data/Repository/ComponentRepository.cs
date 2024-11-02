using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

public interface IComponentRepository
{
    Task<bool> DeleteComponentAsync(Component component);
    Task<Component> GetComponentAsync(int id);
    Task<IEnumerable<Component>> GetAllComponentsAsync();
    Task<Component> SaveComponentAsync(Component component);
}

public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
{
    public async Task<Component> SaveComponentAsync(Component component)
    {
        var exist = await ExistComponent(component.IdComponent);

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

    public async Task<Component> GetComponentAsync(int id)
    {
        var components = await ReadAsync();

        return components.SingleOrDefault(c => c.IdComponent == id)!;
    }

    public async Task<bool> DeleteComponentAsync(Component component)
    {
        return await DeleteAsync(component);
    }

    private async Task<bool> ExistComponent(int? id)
    {
        if (id == null)
            return false;

        var components = await ReadAsync();

        var component = components.FirstOrDefault(c => c.IdComponent == id);

        if (component == default)
            return true;

        return false;
    }
}
    
    

