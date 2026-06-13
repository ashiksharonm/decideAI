using System.ComponentModel;
using System.Text.Json;
using DecideAI.Core.Interfaces;
using Microsoft.SemanticKernel;

namespace DecideAI.API.Plugins;

public class PortfolioDataPlugin
{
    private readonly IProductRepository _productRepository;
    private readonly Services.FinanceService _financeService;

    public PortfolioDataPlugin(IProductRepository productRepository, Services.FinanceService financeService)
    {
        _productRepository = productRepository;
        _financeService = financeService;
    }

    [KernelFunction("get_products_by_brand")]
    [Description("Gets a list of semiconductor products (CPUs/GPUs) manufactured by a specific brand (e.g., Intel, AMD).")]
    public async Task<string> GetProductsByBrandAsync(
        [Description("The brand name to search for (e.g., 'Intel' or 'AMD').")] string brand)
    {
        var products = await _productRepository.GetProductsByBrandAsync(brand);
        return JsonSerializer.Serialize(products);
    }

    [KernelFunction("get_products_by_core_count")]
    [Description("Gets a list of semiconductor products within a specific core count range.")]
    public async Task<string> GetProductsByCoreCountAsync(
        [Description("The minimum number of cores.")] int minCores,
        [Description("The maximum number of cores.")] int maxCores)
    {
        var products = await _productRepository.GetProductsByCoresAsync(minCores, maxCores);
        return JsonSerializer.Serialize(products);
    }

    [KernelFunction("get_company_financials")]
    [Description("Gets real-time financial metrics for a company from the stock market using their ticker symbol.")]
    public async Task<string> GetCompanyFinancialsAsync(
        [Description("The stock ticker symbol of the company (e.g., 'INTC' for Intel, 'AMD' for AMD).")] string ticker)
    {
        var metrics = await _financeService.GetMetricsForTickerAsync(ticker);
        if (metrics == null) return "Financial data not found or API error.";
        return JsonSerializer.Serialize(metrics);
    }
}
