using PAWG1.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace PAWG1.Data.Repository;

public interface IRepositoryBase<T>
{
    Task<bool> CreateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> ExistsAsync(T entity);
    Task<IEnumerable<T>> ReadAsync();
    Task<bool> UpdateAsync(T entity);
}

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly PawprojectContext _dataDbContext;

    protected PawprojectContext DbContext => _dataDbContext;

    public RepositoryBase()
    {
        _dataDbContext = new PawprojectContext();
    }

    public async Task<bool> CreateAsync(T entity)
    {
        try
        {
            await _dataDbContext.AddAsync(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in CreateAsync");
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _dataDbContext.Update(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in UpdateAsync");
        }
    }

    public async Task<bool> UpdateManyAsync(IEnumerable<T> entities)
    {
        try
        {
            _dataDbContext.UpdateRange(entities);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in UpdateManyAsync");
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            _dataDbContext.Remove(entity);
            return await SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is an error in DeleteAsync");
        }
    }

    public async Task<IEnumerable<T>> ReadAsync()
    {
        try
        {
            var query = _dataDbContext.Set<T>().AsQueryable();

            // Verifica si el tipo T es 'Component'
            if (typeof(T) == typeof(Component))
            {
                // Agrega el 'Include' solo si el tipo es 'Component'
                query = query.Include("Users"); // Asumiendo que 'Users' es la relación que quieres cargar
            }

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("There is an error in ReadAsync", ex);
        }
    }

    public async Task<bool> ExistsAsync(T entity)
    {
        try
        {
            var items = await ReadAsync();
            return items.Any(x => x.Equals(entity));
        }
        catch (Exception ex)
        {
            throw new Exception("There is a error in ExistsAsync");
        }
    }

    protected async Task<bool> SaveAsync()
    {
        var result = await _dataDbContext.SaveChangesAsync();
        return result > 0;
    }


}
