using DecideAI.API.Plugins;
using DecideAI.API.Services;
using DecideAI.Core.Interfaces;
using DecideAI.Core.Models;
using Moq;
using Xunit;

namespace DecideAI.Tests;

public class PortfolioDataPluginTests
{
    private readonly Mock<IProductRepository> _mockProductRepo;
    private readonly FinanceService _financeService; // In a real scenario we'd mock a finance interface, but this is sufficient.
    private readonly PortfolioDataPlugin _sut;

    public PortfolioDataPluginTests()
    {
        _mockProductRepo = new Mock<IProductRepository>();
        _financeService = new FinanceService(); // Mocking actual HTTP calls would require mocking HttpClient or using an interface.
        _sut = new PortfolioDataPlugin(_mockProductRepo.Object, _financeService);
    }

    [Fact]
    public async Task GetProductsByBrandAsync_ReturnsSerializedProducts()
    {
        // Arrange
        var mockProducts = new List<Product>
        {
            new Product { Brand = "Intel", Model = "Core i9-14900K", Cores = 24 }
        };
        _mockProductRepo.Setup(repo => repo.GetProductsByBrandAsync("Intel"))
            .ReturnsAsync(mockProducts);

        // Act
        var result = await _sut.GetProductsByBrandAsync("Intel");

        // Assert
        Assert.Contains("Core i9-14900K", result);
        Assert.Contains("24", result);
    }

    [Fact]
    public async Task GetProductsByCoreCountAsync_ReturnsSerializedProducts()
    {
        // Arrange
        var mockProducts = new List<Product>
        {
            new Product { Brand = "AMD", Model = "Ryzen 9 7950X", Cores = 16 }
        };
        _mockProductRepo.Setup(repo => repo.GetProductsByCoresAsync(10, 20))
            .ReturnsAsync(mockProducts);

        // Act
        var result = await _sut.GetProductsByCoreCountAsync(10, 20);

        // Assert
        Assert.Contains("Ryzen 9 7950X", result);
    }
}
