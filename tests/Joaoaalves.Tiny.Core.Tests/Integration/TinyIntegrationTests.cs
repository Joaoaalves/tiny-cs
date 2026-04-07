using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Orders;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Products;
using Joaoaalves.Tiny.Abstractions.DTOs.Requests.Stock;
using Joaoaalves.Tiny.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.Tiny.Abstractions.Interfaces;

namespace Joaoaalves.Tiny.Core.Tests.Integration;

/// <summary>
/// Real API integration tests. These require a valid Tiny API token passed via
/// the TINY_TOKEN environment variable and are excluded from the standard test run.
/// </summary>
[Trait("Category", "Integration")]
public class TinyIntegrationTests
{
    private static IServiceProvider BuildProvider()
    {
        var token = Environment.GetEnvironmentVariable("TINY_TOKEN")
            ?? throw new InvalidOperationException(
                "Set TINY_TOKEN environment variable to a valid Tiny API token before running integration tests.");

        return new ServiceCollection()
            .AddTiny(token)
            .BuildServiceProvider();
    }

    [Fact]
    public async Task ProductService_SearchAsync_ReturnsAtLeastOnePage()
    {
        var provider = BuildProvider();
        var service = provider.GetRequiredService<ITinyProductService>();

        var result = await service.SearchAsync(new SearchProductsRequest { Page = 1 });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 1);
    }

    [Fact]
    public async Task OrderService_SearchAsync_ReturnsAtLeastOnePage()
    {
        var provider = BuildProvider();
        var service = provider.GetRequiredService<ITinyOrderService>();

        var result = await service.SearchAsync(new SearchOrdersRequest { Page = 1 });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 1);
    }

    [Fact]
    public async Task StockService_ListUpdatesAsync_ReturnsResult()
    {
        var provider = BuildProvider();
        var service = provider.GetRequiredService<ITinyStockService>();

        var result = await service.ListUpdatesAsync(new ListStockUpdatesRequest
        {
            UpdatedSince = DateTime.Today.AddDays(-7),
            Page = 1
        });

        Assert.NotNull(result);
        Assert.True(result.TotalPages >= 0);
    }
}
