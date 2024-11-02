using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAWG1.Models.EFModels;
namespace PAWG1.Data.Repository;

    public class ComponentRepository : RepositoryBase<Component>
{
 

    public async Task<Component> SaveProductAsync(Component component)
    {
        var exists = component.IdComponent != null && component.IdComponent > 0;
        if (exists)
            await UpdateAsync(component);
        else
            await CreateAsync(component);
        var updated = await ReadAsync();
        return updated.SingleOrDefault(x => x.IdComponent == component.IdComponent)!;
    }

}
    
    

