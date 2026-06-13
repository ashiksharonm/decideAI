using DecideAI.Core.Models;

namespace DecideAI.Core.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand);
    Task<IEnumerable<Product>> GetProductsByCoresAsync(int minCores, int maxCores);
    Task<Product?> GetProductByModelAsync(string model);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task InsertManyAsync(IEnumerable<Product> products);
    Task<bool> IsEmptyAsync();
}
