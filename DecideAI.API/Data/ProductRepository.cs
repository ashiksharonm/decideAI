using DecideAI.Core.Interfaces;
using DecideAI.Core.Models;
using MongoDB.Driver;

namespace DecideAI.API.Data;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoDatabase database)
    {
        _products = database.GetCollection<Product>("Products");
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandAsync(string brand)
    {
        return await _products.Find(p => p.Brand.ToLower() == brand.ToLower()).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCoresAsync(int minCores, int maxCores)
    {
        return await _products.Find(p => p.Cores >= minCores && p.Cores <= maxCores).ToListAsync();
    }

    public async Task<Product?> GetProductByModelAsync(string model)
    {
        return await _products.Find(p => p.Model.ToLower().Contains(model.ToLower())).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task InsertManyAsync(IEnumerable<Product> products)
    {
        if (products.Any())
        {
            await _products.InsertManyAsync(products);
        }
    }

    public async Task<bool> IsEmptyAsync()
    {
        var count = await _products.CountDocumentsAsync(_ => true);
        return count == 0;
    }
}
