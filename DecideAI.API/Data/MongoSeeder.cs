using DecideAI.Core.Interfaces;
using DecideAI.Core.Models;

namespace DecideAI.API.Data;

public class MongoSeeder
{
    private readonly IProductRepository _productRepository;

    public MongoSeeder(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task SeedAsync()
    {
        if (await _productRepository.IsEmptyAsync())
        {
            // Real-world CPU Data (Not Synthetic)
            var realProducts = new List<Product>
            {
                // Intel Client
                new Product { Brand = "Intel", Model = "Core i9-14900K", Cores = 24, Threads = 32, BaseClockGHz = 3.2, TdpW = 125 },
                new Product { Brand = "Intel", Model = "Core i7-14700K", Cores = 20, Threads = 28, BaseClockGHz = 3.4, TdpW = 125 },
                new Product { Brand = "Intel", Model = "Core i5-14600K", Cores = 14, Threads = 20, BaseClockGHz = 3.5, TdpW = 125 },
                new Product { Brand = "Intel", Model = "Core i9-13900K", Cores = 24, Threads = 32, BaseClockGHz = 3.0, TdpW = 125 },
                
                // Intel Server (Xeon)
                new Product { Brand = "Intel", Model = "Xeon Platinum 8490H", Cores = 60, Threads = 120, BaseClockGHz = 1.9, TdpW = 350 },
                new Product { Brand = "Intel", Model = "Xeon Gold 6430", Cores = 32, Threads = 64, BaseClockGHz = 2.1, TdpW = 270 },

                // AMD Client
                new Product { Brand = "AMD", Model = "Ryzen 9 7950X3D", Cores = 16, Threads = 32, BaseClockGHz = 4.2, TdpW = 120 },
                new Product { Brand = "AMD", Model = "Ryzen 9 7950X", Cores = 16, Threads = 32, BaseClockGHz = 4.5, TdpW = 170 },
                new Product { Brand = "AMD", Model = "Ryzen 7 7800X3D", Cores = 8, Threads = 16, BaseClockGHz = 4.2, TdpW = 120 },
                new Product { Brand = "AMD", Model = "Ryzen 5 7600X", Cores = 6, Threads = 12, BaseClockGHz = 4.7, TdpW = 105 },

                // AMD Server (EPYC)
                new Product { Brand = "AMD", Model = "EPYC 9654", Cores = 96, Threads = 192, BaseClockGHz = 2.4, TdpW = 360 },
                new Product { Brand = "AMD", Model = "EPYC 9754", Cores = 128, Threads = 256, BaseClockGHz = 2.25, TdpW = 360 }
            };

            await _productRepository.InsertManyAsync(realProducts);
        }
    }
}
