using PAWG1.Models.EFModels;
using PAWG1.Data.Repository;

namespace PAWG1.Service.Services;

public interface IComponentService
{
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductAsync(int id);
    Task<Product> SaveProductAsync(Product product);
}

/// <summary>
/// Interface for product-related services.
/// </summary>



public class ComponentService : IComponentService, IComponentService
{
    private readonly IComponentRepository _componentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="productRepository">The repository used to access product data.</param>
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    /// <summary>
    /// Asynchronously saves a new product into the database.
    /// </summary>
	/// <param name="product">The product to be saved.</param>
    /// <returns>A task that represents the asynchronous operation, containing all <see cref="Product"/>.</returns>
    public async Task<Product> SaveProductAsync(Product product)
    {
        return await _productRepository.SaveProductAsync(product);
    }

}

